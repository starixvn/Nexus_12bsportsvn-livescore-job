namespace SabaNews.Livescore.Services.Criteria.Match
{
    using Core.Enum;
    using Fanex.Data.Repository;
    using System;

    public class GetHotLeagueMatchTodayCriteria : CriteriaBase
    {
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public short SportId { get; set; } = (short)SportType.Soccer;

        public override string GetSettingKey() => "Livescore_Match_GetTodayHotLeague";

        public override bool IsValid() => true;
    }
}