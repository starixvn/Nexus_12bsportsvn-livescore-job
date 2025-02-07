using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Season
{
    public class ImportSeasonPlayerHonorCommand : BaseNonQueryCommand
    {
        public string PlayerHonorListJson { get; set; }

        public override string GetSettingKey() => "Livescore_Sync_SeasonPlayerHonor_Import";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(PlayerHonorListJson);
    }
}