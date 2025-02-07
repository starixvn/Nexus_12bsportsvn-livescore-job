namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncCountryListWorker : WorkerBase
    {
        private readonly ISyncCountryService syncCountryService;

        public SyncCountryListWorker(
            ISyncCountryService syncCountryService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncCountryService = syncCountryService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncCountryList);
        }

        public override async Task DoWorkAsync()
        {
            await syncCountryService.SyncCountryListAsync();
        }
    }
}