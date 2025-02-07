namespace SabaNews.Livescore.Models.Match
{
    using Lineup;
    using System.Collections.Generic;

    public class MatchDetail
    {
        public Match GeneralInfo { get; set; }
        public Statistic Statistic { get; set; }
        public IEnumerable<MatchIncident> Incidents { get; set; }
        public LineupDetail Lineup { get; set; }
        public HeadToHeadDetail HeadToHead { get; set; }
    }
}