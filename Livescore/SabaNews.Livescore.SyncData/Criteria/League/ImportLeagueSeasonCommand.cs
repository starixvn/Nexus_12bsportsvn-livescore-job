using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.League
{
    public class ImportLeagueSeasonCommand : BaseNonQueryCommand
    {
        public string LeagueListJson { get; set; }

        public override string GetSettingKey() => "Livescore_Sync_LeagueSeason_Import";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(LeagueListJson);
    }
}