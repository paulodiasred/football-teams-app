using Newtonsoft.Json;

namespace FootballTeamsApp.Models
{
    public class TeamStanding
    {
        public int Id { get; set; }

        [JsonProperty("team_id")]
        public int TeamId { get; set; }
        [JsonProperty("team_name")]
        public string TeamName { get; set; }
        [JsonProperty("team_badge")]
        public string TeamBadge { get; set; }
        [JsonProperty("overall_league_position")]
        public int OverallLeaguePosition { get; set; }
        [JsonProperty("overall_league_payed")]
        public int OverallLeaguePlayed { get; set; }
        [JsonProperty("overall_league_W")]
        public int OverallLeagueWins { get; set; }
        [JsonProperty("overall_league_D")]
        public int OverallLeagueDraws { get; set; }
        [JsonProperty("overall_league_L")]
        public int OverallLeagueLosses { get; set; }
        [JsonProperty("overall_league_GF")]
        public int OverallLeagueGoalsFor { get; set; }
        [JsonProperty("overall_league_GA")]
        public int OverallLeagueGoalsAgainst { get; set; }
        [JsonProperty("overall_league_PTS")]
        public int OverallLeaguePoints { get; set; }
        [JsonProperty("overall_promotion")]
        public string OverallPromotion { get; set; }
    }
}
