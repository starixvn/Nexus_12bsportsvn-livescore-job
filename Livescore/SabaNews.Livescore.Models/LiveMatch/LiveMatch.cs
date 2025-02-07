namespace SabaNews.Livescore.Models.LiveMatch
{
    using Lineup;
    using Match;
    using System.Collections.Generic;

    public class LiveMatch : Match
    {
        public TeamStatistic HomeStatistic { get; set; }
        public TeamStatistic AwayStatistic { get; set; }
        public IEnumerable<MatchIncident> Incidents { get; set; }
        public LineupDetail Lineup { get; set; }
    }
}