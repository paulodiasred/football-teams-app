using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballTeamsApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int TeamId { get; set; } // Identificador da API (team_id)
        public string team_key { get; set; }
        public string team_name { get; set; }
        public string team_country { get; set; }
        public string team_founded { get; set; }
        public string team_badge { get; set; }

        // Relação de um-para-um com Coach
        public Coach Coach { get; set; }

        // Chave estrangeira para Venue
        public int? VenueId { get; set; }  // Propriedade para armazenar o Id do Venue
        public Venue Venue { get; set; } // Propriedade de navegação para o Venue

        // Lista de jogadores
        public List<Player> Players { get; set; } = new List<Player>();

        // Propriedade transitória para receber os técnicos da API
        [NotMapped]
        public List<Coach> coaches { get; set; } // Esta propriedade não será mapeada para o banco de dados
    }

    public class Venue
    {
        public int VenueId { get; set; }  // Chave primária
        public string venue_name { get; set; }
        public string venue_address { get; set; }
        public string venue_city { get; set; }
        public string venue_capacity { get; set; }
        public string venue_surface { get; set; }
    }

    public class Player
    {
        public int PlayerId { get; set; }
        public long player_key { get; set; }
        public string player_id { get; set; }
        public string player_image { get; set; }
        public string player_name { get; set; }
        public string player_number { get; set; }
        public string player_country { get; set; }
        public string player_type { get; set; }
        public string player_age { get; set; }
        public string player_match_played { get; set; }
        public string player_goals { get; set; }
        public string player_yellow_cards { get; set; }
        public string player_red_cards { get; set; }
        public string player_injured { get; set; }
        public string player_substitute_out { get; set; }
        public string player_substitutes_on_bench { get; set; }
        public string player_assists { get; set; }
        public string player_birthdate { get; set; }
        public string player_is_captain { get; set; }
        public string player_shots_total { get; set; }
        public string player_goals_conceded { get; set; }
        public string player_fouls_committed { get; set; }
        public string player_tackles { get; set; }
        public string player_blocks { get; set; }
        public string player_crosses_total { get; set; }
        public string player_interceptions { get; set; }
        public string player_clearances { get; set; }
        public string player_dispossesed { get; set; }
        public string player_saves { get; set; }
        public string player_inside_box_saves { get; set; }
        public string player_duels_total { get; set; }
        public string player_duels_won { get; set; }
        public string player_dribble_attempts { get; set; }
        public string player_dribble_succ { get; set; }
        public string player_pen_comm { get; set; }
        public string player_pen_won { get; set; }
        public string player_pen_scored { get; set; }
        public string player_pen_missed { get; set; }
        public string player_passes { get; set; }
        public string player_passes_accuracy { get; set; }
        public string player_key_passes { get; set; }
        public string player_woordworks { get; set; }
        public string player_rating { get; set; }

        // Relacionamento com Team
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }

    public class Coach
    {
        public int Id { get; set; } // Chave primária
        public string coach_name { get; set; }
        public string coach_country { get; set; }
        public string coach_age { get; set; }

        // Propriedade de navegação para Team
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
