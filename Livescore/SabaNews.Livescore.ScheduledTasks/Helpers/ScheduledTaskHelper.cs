namespace SabaNews.Livescore.ScheduledTasks.Helpers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using SabaNews.Livescore.Provider.API.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ScheduledTaskHelper
    {
        public static ScheduledTaskSettings GetJobConfig(IEnumerable<ScheduledTaskSettings> jobConfigs, string jobName)
        {
            return jobConfigs.FirstOrDefault(job => job.Name.Equals(jobName, StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool IsEnabledJob(IEnumerable<ScheduledTaskSettings> jobConfigs, string jobName)
        {
            var settings = GetJobConfig(jobConfigs, jobName);

            return settings != null && settings.Enabled;
        }

        public static void RegisterDI(this IServiceCollection services, IConfiguration configuration)
        {
            ProviderApiHelpers.RegisterServices(services, configuration);
        }
    }
}