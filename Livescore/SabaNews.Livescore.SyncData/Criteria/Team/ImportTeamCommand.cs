using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Team
{
    public class ImportTeamCommand : BaseNonQueryCommand
    {
        public string TeamListJson { get; set; }

        public override string GetSettingKey() => "Livescore_Sync_Team_Import";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(TeamListJson);
    }
}