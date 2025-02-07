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

    public class SyncIncidentByUpdatedTimeWorkerTests
    {
        private readonly ISyncMatchService syncMatchService;
        private readonly Fixture fixture;
        private readonly List<ScheduledTaskSettings> jobSettings;
        private readonly ILogServiceProvider logService;
        private readonly SyncIncidentByUpdatedTimeWorker worker;

        public SyncIncidentByUpdatedTimeWorkerTests()
        {
            syncMatchService = Substitute.For<ISyncMatchService>();
            logService = Substitute.For<ILogServiceProvider>();
            fixture = new Fixture();
            jobSettings = fixture.Create<List<ScheduledTaskSettings>>();
            worker = new SyncIncidentByUpdatedTimeWorker(syncMatchService, jobSettings, logService);
        }

        [Fact]
        public async Task ExecuteJobAsync_WhenCalled_CalledExpectedFunction()
        {
            //Act
            await worker.DoWorkAsync();

            //Assert
            await syncMatchService.Received().SyncIncidentByUpdatedTimeAsync();
        }
    }
}