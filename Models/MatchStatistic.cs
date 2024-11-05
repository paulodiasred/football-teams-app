using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FootballTeamsApp.Models
{
    public class MatchStatistic
    {
        public int Id { get; set; }
        public int MatchId { get; set; } // Propriedade para a chave estrangeira
        public Match Match { get; set; } // Navegação para a entidade Match

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("home")]
        public string Home { get; set; }

        [JsonProperty("away")]
        public string Away { get; set; }

    }
}
