using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Player
{
    public class ImportPlayerCommand : BaseNonQueryCommand
    {
        public string PlayerListJson { get; set; }

        public override string GetSettingKey() => "Livescore_Sync_Player_Import";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(PlayerListJson);
    }
}