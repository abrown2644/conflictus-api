using Microsoft.EntityFrameworkCore.Migrations;

namespace Conflictus.Migrations
{
    public partial class DBInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasCoordinates = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlagUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SideA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Commanders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Losses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Victory = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SideB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Commanders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Losses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Victory = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SideB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "War",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_War", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantSideA",
                columns: table => new
                {
                    ParticipantsId = table.Column<int>(type: "int", nullable: false),
                    SideAsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantSideA", x => new { x.ParticipantsId, x.SideAsId });
                    table.ForeignKey(
                        name: "FK_ParticipantSideA_Participant_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantSideA_SideA_SideAsId",
                        column: x => x.SideAsId,
                        principalTable: "SideA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantSideB",
                columns: table => new
                {
                    ParticipantsId = table.Column<int>(type: "int", nullable: false),
                    SideBsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantSideB", x => new { x.ParticipantsId, x.SideBsId });
                    table.ForeignKey(
                        name: "FK_ParticipantSideB_Participant_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantSideB_SideB_SideBsId",
                        column: x => x.SideBsId,
                        principalTable: "SideB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SideAId = table.Column<int>(type: "int", nullable: true),
                    SideBId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battle_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Battle_SideA_SideAId",
                        column: x => x.SideAId,
                        principalTable: "SideA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Battle_SideB_SideBId",
                        column: x => x.SideBId,
                        principalTable: "SideB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Battle_War_WarId",
                        column: x => x.WarId,
                        principalTable: "War",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantWar",
                columns: table => new
                {
                    ParticipantsId = table.Column<int>(type: "int", nullable: false),
                    WarsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantWar", x => new { x.ParticipantsId, x.WarsId });
                    table.ForeignKey(
                        name: "FK_ParticipantWar_Participant_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantWar_War_WarsId",
                        column: x => x.WarsId,
                        principalTable: "War",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battle_LocationId",
                table: "Battle",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_SideAId",
                table: "Battle",
                column: "SideAId");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_SideBId",
                table: "Battle",
                column: "SideBId");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_WarId",
                table: "Battle",
                column: "WarId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantSideA_SideAsId",
                table: "ParticipantSideA",
                column: "SideAsId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantSideB_SideBsId",
                table: "ParticipantSideB",
                column: "SideBsId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantWar_WarsId",
                table: "ParticipantWar",
                column: "WarsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battle");

            migrationBuilder.DropTable(
                name: "ParticipantSideA");

            migrationBuilder.DropTable(
                name: "ParticipantSideB");

            migrationBuilder.DropTable(
                name: "ParticipantWar");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "SideA");

            migrationBuilder.DropTable(
                name: "SideB");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "War");
        }
    }
}
