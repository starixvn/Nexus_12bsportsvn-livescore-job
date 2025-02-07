namespace SabaNews.Livescore.ScheduledTasks.Tests.Workers
{
    using AutoFixture;
    using Models;
    using NSubstitute;
    using SabaNews.Livescore.Core.Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.ScheduledTasks.Workers;
    using SabaNews.Livescore.SyncData.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class SyncMatchByUpdatedTimeFromCurrentToFutureWorkerTests
    {
        private readonly ISyncMatchService syncMatchService;
        private readonly AppSettings appSettings;
        private readonly Fixture fixture;
        private readonly List<ScheduledTaskSettings> jobSettings;
        private readonly ILogServiceProvider logService;
        private readonly SyncMatchByUpdatedTimeFromCurrentToFutureWorker worker;

        public SyncMatchByUpdatedTimeFromCurrentToFutureWorkerTests()
        {
            syncMatchService = Substitute.For<ISyncMatchService>();
            logService = Substitute.For<ILogServiceProvider>();
            fixture = new Fixture();
            appSettings = fixture.Create<AppSettings>();
            jobSettings = fixture.Create<List<ScheduledTaskSettings>>();
            worker = new SyncMatchByUpdatedTimeFromCurrentToFutureWorker(syncMatchService, jobSettings, appSettings, logService);
        }

        [Fact]
        public async Task DoWorkAsync_WhenUpdatedTimeNotStuck_CallExpectedService()
        {
            var updatedAt = fixture.Create<DateTime>();
            syncMatchService.GetMatchLastUpdatedTimeInDateRangeAsync(Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(updatedAt);

            //actual
            await worker.DoWorkAsync();

            //Expected

            await syncMatchService.Received().SyncAllMatchInfoByStartTimeRangeAndUpdatedTimeAsync(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Is(updatedAt));
        }
    }
}