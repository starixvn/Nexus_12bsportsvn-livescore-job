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

    public class SyncTeamListByLastUpdatedTimeWorkerTests
    {
        private readonly ISyncTeamService syncTeamService;
        private readonly Fixture fixture;
        private readonly List<ScheduledTaskSettings> jobSettings;
        private readonly ILogServiceProvider logService;
        private readonly SyncTeamListByLastUpdatedTimeWorker worker;

        public SyncTeamListByLastUpdatedTimeWorkerTests()
        {
            syncTeamService = Substitute.For<ISyncTeamService>();
            logService = Substitute.For<ILogServiceProvider>();
            fixture = new Fixture();
            jobSettings = fixture.Create<List<ScheduledTaskSettings>>();
            worker = new SyncTeamListByLastUpdatedTimeWorker(syncTeamService, jobSettings, logService);
        }

        [Fact]
        public async Task ExecuteJobAsync_WhenCalled_CalledExpectedFunction()
        {
            //Act
            await worker.DoWorkAsync();

            //Assert
            await syncTeamService.Received().SyncTeamListByLastUpdatedTimeAsync();
        }
    }
}