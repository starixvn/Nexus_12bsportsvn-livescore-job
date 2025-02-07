using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Season
{
    public class ImportSeasonTeamHonorCommand : BaseNonQueryCommand
    {
        public string TeamHonorListJson { get; set; }

        public override string GetSettingKey() => "Livescore_Sync_SeasonTeamHonor_Import";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(TeamHonorListJson);
    }
}