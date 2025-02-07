using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.HeadToHead
{
    public class ImportHeadToHeadCommand : BaseNonQueryCommand
    {
        public string HeadToHeadJson { get; set; }

        public override string GetSettingKey() => "Livescore_SyncHeadToHead_Import";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(HeadToHeadJson);
    }
}