using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Matches
{
    public class ImportMatchStatisticCommand : BaseNonQueryCommand
    {
        public string MatchScoreJson { get; set; }

        public string MatchDetailJson { get; set; }

        public override string GetSettingKey() => "Livescore_SyncMatch_StatisticImport";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(MatchDetailJson) && !string.IsNullOrWhiteSpace(MatchScoreJson);
    }
}