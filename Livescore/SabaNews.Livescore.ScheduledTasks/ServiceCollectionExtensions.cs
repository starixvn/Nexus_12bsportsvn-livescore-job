using Fanex.Caching;
using Fanex.Caching.Redis;
using Fanex.Data;
using Fanex.Data.MySql;
using Fanex.Data.Repository;
using Fanex.Logging;
using Fanex.Logging.Sentry;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet.Client.Options;
using SabaNews.Livescore.Core.Models;
using SabaNews.Livescore.Core.Repositories;
using SabaNews.Livescore.Core.Services;
using SabaNews.Livescore.Provider.API.Helpers;
using SabaNews.Livescore.Repositories;
using SabaNews.Livescore.ScheduledTasks.Models;
using SabaNews.Livescore.ScheduledTasks.MqttClient.Configurations;
using SabaNews.Livescore.ScheduledTasks.MqttClient.Extensions;
using SabaNews.Livescore.ScheduledTasks.MqttClient.Services;
using SabaNews.Livescore.ScheduledTasks.Workers;
using SabaNews.Livescore.Services;
using SabaNews.Livescore.Services.Interfaces;
using SabaNews.Livescore.SyncData.Services;
using SabaNews.Livescore.SyncData.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SabaNews.Livescore.ScheduledTasks
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRedisConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRedisCache(configuration.GetSection("RedisConfig"));

            services.AddDataProtection()
                .SetApplicationName(configuration.GetSection("RedisConfig:ApplicationName").Value)
                .PersistKeysToStackExchangeRedis(
                    ConnectionMultiplexer.Connect(configuration.GetSection("RedisConfig:ConnectionString").Value),
                    "SabaSports-Keys");

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("RedisConfig:ConnectionString").Value;
                options.InstanceName = $"SabaSports.Session-";
            });
        }

        public static void AddConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var connections = configuration.GetSection("ConnectionStrings")
                .GetChildren()
                .AsEnumerable()
                .Where(x => !string.IsNullOrWhiteSpace(x.GetValue<string>("Name")))
                .ToDictionary(x => x.GetValue<string>("Name"),
                    y => new ConnectionConfiguration(y.GetValue<string>("Name"), y.GetValue<string>("ConnectionString"), y.GetValue<string>("ProviderName")));

            DbSettingProviderManager
                .StartNewSession()
                .Use(connections)
                .WithMySql(configuration)
                .UseDefaultDbSettingProvider("~/App_Data/SpNames.xml")
                .Run();

            LogManager
                .SetDefaultLogCategory(configuration.GetSection("LogCategory").Value)
                .Use(new List<ILogEngine>
                {
                    new SentryLogging(new SentryEngineOptions
                    {
                        Dsn = configuration.GetSection("SentryEngineOptions:Dsn").Value
                    })
                });

            services.AddSingleton(configuration);

            var jobSettings = configuration.GetSection(ScheduledTaskSettings.ConfigurationKey).Get<List<ScheduledTaskSettings>>();
            services.AddSingleton(jobSettings);

            var appSettings = configuration.GetSection(AppSettings.ConfigurationKey).Get<AppSettings>();
            services.AddSingleton(appSettings);

            var signalRSettings = configuration.GetSection(PushSignalSettings.ConfigurationKey).Get<PushSignalSettings>();
            services.AddSingleton(signalRSettings);
        }

        public static void AddDependencyInjection(IServiceCollection services, IConfiguration configuration)
        {
            //base service
            services.AddSingleton<IBaseService, BaseService>();
            services.AddSingleton<IDynamicRepository, DynamicRepository>();
            services.AddSingleton<IBaseRepository, BaseRepository>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ICacheWrapperService, CacheWrapperService>();
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<ILogServiceProvider, LogServiceProvider>();

            //Sync service
            services.AddTransient<ISyncLeagueService, SyncLeagueService>();
            services.AddTransient<ISyncTeamService, SyncTeamService>();
            services.AddTransient<ISyncPlayerService, SyncPlayerService>();
            services.AddTransient<ISyncSeasonService, SyncSeasonService>();
            services.AddTransient<ISyncMatchService, SyncMatchService>();
            services.AddTransient<ISyncHeadToHeadService, SyncHeadToHeadService>();
            services.AddTransient<ISyncCountryService, SyncCountryService>();
            ProviderApiHelpers.RegisterServices(services, configuration);

            //livescore service
            services.AddSingleton<ILiveMatchService, LiveMatchService>();
            services.AddSingleton<IMqttClientService, MqttClientService>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IPushSignalService, PushSignalService>();
        }

        public static void AddWorker(IServiceCollection services)
        {
            services.AddHostedService<SyncLeagueListByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncLeagueListGreaterThanLeagueIdWorker>();
            services.AddHostedService<SyncTeamListByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncTeamListGreaterThanTeamIdWorker>();
            services.AddHostedService<SyncPlayerListByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncSeasonListByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncStageListByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncSeasonStandingsByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncSeasonTeamHonorByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncSeasonPlayerHonorByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncMatchByLastUpdatedTimeWorker>();
            services.AddHostedService<SyncLiveMatchThenPushSignalWorker>();
            services.AddHostedService<SyncHeadToHeadFromCurrentDateToFutureWorker>();
            services.AddHostedService<SyncMatchByMaxMatchIdWorker>();
            services.AddHostedService<SyncDeleledMatchesWorker>();
            services.AddHostedService<SyncMatchByUpdatedTimeInPastWorker>();
            services.AddHostedService<SyncMatchByUpdatedTimeFromCurrentToFutureWorker>();
            services.AddHostedService<SyncCountryListWorker>();
            services.AddHostedService<SyncIncidentByUpdatedTimeWorker>();
        }

        public static IServiceCollection AddMqttClientHostedService(IServiceCollection services, IConfiguration configuration)
        {
            var mqttSettings = configuration.GetSection(MqttSettings.ConfigurationKey).Get<MqttSettings>();
            services.AddSingleton(mqttSettings);

            if (mqttSettings.Enabled)
            {
                services.AddMqttClientServiceWithConfig(aspOptionBuilder =>
                {
                    aspOptionBuilder.WithWebSocketServer(o =>
                    {
                        o.Uri = mqttSettings.WebSocketHost;
                        if (mqttSettings.UseProxy)
                        {
                            o.ProxyOptions = new MqttClientWebSocketProxyOptions
                            {
                                Address = mqttSettings.ProxyAddress,
                                BypassOnLocal = mqttSettings.ByPassLocal
                            };
                        }
                    });
                    aspOptionBuilder.WithCredentials(mqttSettings.Username);
                });
            }

            return services;
        }

        private static IServiceCollection AddMqttClientServiceWithConfig(this IServiceCollection services, Action<AspNetCoreMqttClientOptionBuilder> configure)
        {
            services.AddSingleton(serviceProvider =>
            {
                var optionBuilder = new AspNetCoreMqttClientOptionBuilder(serviceProvider);
                configure(optionBuilder);
                return optionBuilder.Build();
            });
            services.AddSingleton<MqttClientService>();
            services.AddSingleton<IHostedService>(serviceProvider =>
            {
                return serviceProvider.GetService<MqttClientService>();
            });

            return services;
        }
    }
}