using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Mqtt
{
    public class SyncMqttMatchResponseCommand : BaseNonQueryCommand
    {
        public string MatchJson { get; set; }

        public override string GetSettingKey() => "Livescore_SyncMatch_ReceivedFromMqtt";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(MatchJson);
    }
}