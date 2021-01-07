using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamApplication.Migrations
{
    public partial class modelv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volunteer_Team_TeamId",
                table: "Volunteer");

            migrationBuilder.DropIndex(
                name: "IX_Volunteer_TeamId",
                table: "Volunteer");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Volunteer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Volunteer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_TeamId",
                table: "Volunteer",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteer_Team_TeamId",
                table: "Volunteer",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
