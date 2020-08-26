using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace TeamApplication.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    EntityId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EntityName = table.Column<string>(maxLength: 50, nullable: false),
                    VisitDay = table.Column<int>(nullable: false),
                    VisitTime = table.Column<DateTime>(nullable: false),
                    VisitDuration = table.Column<TimeSpan>(nullable: false),
                    VisitScale = table.Column<int>(nullable: false),
                    MaxVolunteer = table.Column<int>(nullable: false),
                    Contact = table.Column<string>(maxLength: 30, nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UFAbreviation = table.Column<string>(maxLength: 2, nullable: false),
                    UFName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    JobDay = table.Column<DateTime>(nullable: false),
                    JobPeriod = table.Column<int>(maxLength: 1, nullable: false),
                    ActionKind = table.Column<int>(nullable: false),
                    MaxVolunteer = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Job_Entity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entity",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(maxLength: 100, nullable: false),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    JobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Team_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AddressKind = table.Column<int>(nullable: false),
                    Designation = table.Column<string>(maxLength: 120, nullable: false),
                    District = table.Column<string>(maxLength: 60, nullable: false),
                    Complement = table.Column<string>(maxLength: 60, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 9, nullable: false),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer",
                columns: table => new
                {
                    VolunteerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VDocCPF = table.Column<string>(maxLength: 11, nullable: false),
                    VDocRG = table.Column<string>(maxLength: 9, nullable: false),
                    VName = table.Column<string>(maxLength: 50, nullable: false),
                    VBirthDate = table.Column<DateTime>(nullable: false),
                    VAge = table.Column<int>(nullable: false),
                    VResumee = table.Column<string>(maxLength: 150, nullable: false),
                    VPhone = table.Column<string>(maxLength: 15, nullable: false),
                    VMessagePhone = table.Column<bool>(nullable: false),
                    VEmail = table.Column<string>(maxLength: 50, nullable: false),
                    VSocialMidiaProfile = table.Column<string>(maxLength: 50, nullable: true),
                    VActive = table.Column<bool>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer", x => x.VolunteerId);
                    table.ForeignKey(
                        name: "FK_Volunteer_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Volunteer_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    TeamScheduleId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TSDate = table.Column<DateTime>(nullable: false),
                    TSPeriod = table.Column<string>(maxLength: 1, nullable: false),
                    VolunteerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.TeamScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedule_Volunteer_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteer",
                        principalColumn: "VolunteerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamVolunteer",
                columns: table => new
                {
                    TeamVolunteerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VolunteerId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamVolunteer", x => x.TeamVolunteerId);
                    table.ForeignKey(
                        name: "FK_TeamVolunteer_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamVolunteer_Volunteer_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteer",
                        principalColumn: "VolunteerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_EntityId",
                table: "Job",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_VolunteerId",
                table: "Schedule",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_JobId",
                table: "Team",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamVolunteer_TeamId",
                table: "TeamVolunteer",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamVolunteer_VolunteerId",
                table: "TeamVolunteer",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_AddressId",
                table: "Volunteer",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_TeamId",
                table: "Volunteer",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "TeamVolunteer");

            migrationBuilder.DropTable(
                name: "Volunteer");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Entity");
        }
    }
}
