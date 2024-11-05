using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FootballTeamsApp.Models
{
    public class Goalscorer
    {
        public int Id { get; set; }
        public int MatchId { get; set; } // Propriedade para a chave estrangeira
        public Match Match { get; set; } // Navegação para a entidade Match

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("home_scorer")]
        public string HomeScorer { get; set; }

        [JsonProperty("home_assist")]
        public string HomeAssist { get; set; }

        [JsonProperty("away_scorer")]
        public string AwayScorer { get; set; }

        [JsonProperty("away_assist")]
        public string AwayAssist { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("score_info_time")]
        public string ScoreInfoTime { get; set; }
    }
}
