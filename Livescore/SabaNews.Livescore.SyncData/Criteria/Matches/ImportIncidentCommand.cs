using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Matches
{
    public class ImportIncidentCommand : BaseNonQueryCommand
    {
        public long MatchId { get; set; }
        public string IncidentJson { get; set; }

        public override string GetSettingKey() => "Livescore_Match_Incident_Sync";

        public override bool IsValid() => MatchId > 0 && !string.IsNullOrWhiteSpace(IncidentJson);
    }
}