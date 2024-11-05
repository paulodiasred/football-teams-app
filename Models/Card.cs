using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FootballTeamsApp.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int MatchId { get; set; } // Propriedade para a chave estrangeira
        public Match Match { get; set; } // Navegação para a entidade Match

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("home_fault")]
        public string HomeFault { get; set; }

        [JsonProperty("card")]
        public string CardType { get; set; }

        [JsonProperty("away_fault")]
        public string AwayFault { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }
    }
}
