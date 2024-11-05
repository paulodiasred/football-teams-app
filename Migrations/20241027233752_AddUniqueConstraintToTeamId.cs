using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamsApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToTeamId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamId",
                table: "Teams",
                column: "TeamId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamId",
                table: "Teams");
        }
    }
}
