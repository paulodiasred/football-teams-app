using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamsApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamIdColumnToTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Teams");
        }
    }
}
