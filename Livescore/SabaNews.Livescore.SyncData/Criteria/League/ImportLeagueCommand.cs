using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.League
{
    public class ImportLeagueCommand : BaseNonQueryCommand
    {
        public string LeagueListJson { get; set; }

        public override string GetSettingKey() => "Livescore_Sync_League_Import";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(LeagueListJson);
    }
}