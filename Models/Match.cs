using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FootballTeamsApp.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int TeamId { get; set; } // Chave estrangeira para Team
        public Team Team { get; set; } // Navegação para o Team

        [JsonProperty("league_name")]
        public string LeagueName { get; set; }

        [JsonProperty("match_date")]
        public string MatchDate { get; set; }

        [JsonProperty("match_hometeam_name")]
        public string HomeTeam { get; set; }

        [JsonProperty("team_home_badge")]
        public string HomeTeamBadge { get; set; }

        [JsonProperty("team_away_badge")]
        public string AwayTeamBadge { get; set; }

        [JsonProperty("match_awayteam_name")]
        public string AwayTeam { get; set; }

        [JsonProperty("match_hometeam_score")]
        public int? HomeScore { get; set; }

        [JsonProperty("match_awayteam_score")]
        public int? AwayScore { get; set; }

        [JsonProperty("match_stadium")]
        public string MatchStadium { get; set; }

        [JsonProperty("match_referee")]
        public string MatchReferee { get; set; }

        [JsonProperty("cards")]
        public List<Card> Cards { get; set; } = new List<Card>();

        [JsonProperty("goalscorer")]
        public List<Goalscorer> Goalscorers { get; set; } = new List<Goalscorer>();

        [JsonProperty("statistics")]
        public List<MatchStatistic> MatchStatistics { get; set; } = new List<MatchStatistic>();

        [Timestamp]
        public byte[] RowVersion { get; set; }  // Controle de versão para concorrência
    }
}
