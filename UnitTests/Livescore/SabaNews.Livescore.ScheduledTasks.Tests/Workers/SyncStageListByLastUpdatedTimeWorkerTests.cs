namespace SabaNews.Livescore.ScheduledTasks.Tests.Workers
{
    using AutoFixture;
    using Models;
    using NSubstitute;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.ScheduledTasks.Workers;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class SyncStageListByLastUpdatedTimeWorkerTests
    {
        private readonly ISyncSeasonService syncSeasonService;
        private readonly Fixture fixture;
        private readonly List<ScheduledTaskSettings> jobSettings;
        private readonly ILogServiceProvider logService;
        private readonly SyncStageListByLastUpdatedTimeWorker worker;

        public SyncStageListByLastUpdatedTimeWorkerTests()
        {
            syncSeasonService = Substitute.For<ISyncSeasonService>();
            logService = Substitute.For<ILogServiceProvider>();
            fixture = new Fixture();
            jobSettings = fixture.Create<List<ScheduledTaskSettings>>();
            worker = new SyncStageListByLastUpdatedTimeWorker(syncSeasonService, jobSettings, logService);
        }

        [Fact]
        public async Task ExecuteJobAsync_WhenCalled_CalledExpectedFunction()
        {
            //Act
            await worker.DoWorkAsync();

            //Assert
            await syncSeasonService.Received().SyncStageListByLastUpdatedTimeAsync();
        }
    }
}