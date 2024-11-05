using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamsApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamStandings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamBadge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Played = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Promotion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStandings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    venue_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    venue_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    venue_city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    venue_capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    venue_surface = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    team_key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_founded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    team_badge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VenueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coach_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coach_country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    coach_age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coaches_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeTeamBadge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayTeamBadge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeScore = table.Column<int>(type: "int", nullable: true),
                    AwayScore = table.Column<int>(type: "int", nullable: true),
                    MatchStadium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatchReferee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    player_key = table.Column<long>(type: "bigint", nullable: false),
                    player_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_match_played = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_goals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_yellow_cards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_red_cards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_injured = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_substitute_out = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_substitutes_on_bench = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_assists = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_birthdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_is_captain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_shots_total = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_goals_conceded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_fouls_committed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_tackles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_blocks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_crosses_total = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_interceptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_clearances = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_dispossesed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_saves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_inside_box_saves = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_duels_total = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_duels_won = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_dribble_attempts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_dribble_succ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_pen_comm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_pen_won = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_pen_scored = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_pen_missed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_passes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_passes_accuracy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_key_passes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_woordworks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    player_rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeFault = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayFault = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goalscorers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeScorer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAssist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayScorer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwayAssist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScoreInfoTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goalscorers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goalscorers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Away = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchStatistics_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_MatchId",
                table: "Cards",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_TeamId",
                table: "Coaches",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goalscorers_MatchId",
                table: "Goalscorers",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamId",
                table: "Matches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchStatistics_MatchId",
                table: "MatchStatistics",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamId",
                table: "Player",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_VenueId",
                table: "Teams",
                column: "VenueId",
                unique: true,
                filter: "[VenueId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Goalscorers");

            migrationBuilder.DropTable(
                name: "MatchStatistics");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "TeamStandings");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
