namespace SabaNews.Livescore.ScheduledTasks.Tests.Workers
{
    using AutoFixture;
    using Models;
    using NSubstitute;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.ScheduledTasks.Workers;
    using SabaNews.Livescore.SyncData.Services.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class SyncCountryListWorkerTests
    {
        private readonly ISyncCountryService syncCountryService;
        private readonly Fixture fixture;
        private readonly List<ScheduledTaskSettings> jobSettings;
        private readonly ILogServiceProvider logService;
        private readonly SyncCountryListWorker worker;

        public SyncCountryListWorkerTests()
        {
            syncCountryService = Substitute.For<ISyncCountryService>();
            logService = Substitute.For<ILogServiceProvider>();
            fixture = new Fixture();
            jobSettings = fixture.Create<List<ScheduledTaskSettings>>();
            worker = new SyncCountryListWorker(syncCountryService, jobSettings, logService);
        }

        [Fact]
        public async Task ExecuteJobAsync_WhenCalled_CalledExpectedFunction()
        {
            //Act
            await worker.DoWorkAsync();

            //Assert
            await syncCountryService.Received().SyncCountryListAsync();
        }
    }
}