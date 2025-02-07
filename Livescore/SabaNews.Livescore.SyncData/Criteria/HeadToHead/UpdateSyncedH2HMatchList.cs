using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.HeadToHead
{
    public class UpdateSyncedH2HMatchList : BaseNonQueryCommand
    {
        public long MatchId { get; set; }

        public override string GetSettingKey() => "Livescore_SyncHeadToHead_UpdateSyncedMatch";

        public override bool IsValid() => MatchId > 0;
    }
}