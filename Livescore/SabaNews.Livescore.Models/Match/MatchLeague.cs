namespace SabaNews.Livescore.Models.Match
{
    using League;
    using System.Collections.Generic;

    public class MatchLeague
    {
        public IList<Match> Matches { get; set; }

        public League League { get; set; }
    }
}