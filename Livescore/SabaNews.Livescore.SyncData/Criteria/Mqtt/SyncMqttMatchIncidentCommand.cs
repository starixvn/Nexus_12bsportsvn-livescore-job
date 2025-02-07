using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.SyncData.Criteria.Mqtt
{
    public class SyncMqttMatchIncidentCommand : BaseNonQueryCommand
    {
        public string IncindentJson { get; set; }

        public override string GetSettingKey() => "Livescore_SyncIncident_ReceivedFromMqtt";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(IncindentJson);
    }
}