using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FootballTeamsApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamStandingSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TeamStandings",
                columns: new[] { "Id", "Draws", "GoalsAgainst", "GoalsFor", "Losses", "Played", "Points", "Position", "Promotion", "TeamBadge", "TeamId", "TeamName", "Wins" },
                values: new object[,]
                {
                    { 1, 5, 0, 0, 5, 0, 30, 0, null, null, 1, null, 10 },
                    { 2, 4, 0, 0, 6, 0, 28, 0, null, null, 2, null, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeamStandings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TeamStandings",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
