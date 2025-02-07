using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Matches
{
    public class ImportDeletedMatchCommand : BaseNonQueryCommand
    {
        public string DeletedMatchJson { get; set; }

        public override string GetSettingKey() => "Livescore_SyncMatch_SyncDeteledMatch";

        public override bool IsValid()
             => !string.IsNullOrWhiteSpace(DeletedMatchJson);
    }
}