namespace SabaNews.Livescore.Services.Criteria.Match
{
    using Core.Enum;
    using Fanex.Data.Repository;

    public class GetSummaryMatchByIdCriteria : CriteriaBase
    {
        public long MatchId { get; set; }

        public short SportId { get; set; } = (short)SportType.Soccer;

        public override string GetSettingKey() => "Livescore_Match_GetSummaryById";

        public override bool IsValid() => MatchId > 0;
    }
}