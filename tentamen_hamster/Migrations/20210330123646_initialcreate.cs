using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tentamen_hamster.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cages",
                columns: table => new
                {
                    CageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cages", x => x.CageId);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSpace",
                columns: table => new
                {
                    ExerciseSpaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSpace", x => x.ExerciseSpaceId);
                });

            migrationBuilder.CreateTable(
                name: "Hamsters",
                columns: table => new
                {
                    HamsterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCheckedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeLastExercise = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CageId = table.Column<int>(type: "int", nullable: true),
                    ExerciseSpaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamsters", x => x.HamsterId);
                    table.ForeignKey(
                        name: "FK_Hamsters_Cages_CageId",
                        column: x => x.CageId,
                        principalTable: "Cages",
                        principalColumn: "CageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hamsters_ExerciseSpace_ExerciseSpaceId",
                        column: x => x.ExerciseSpaceId,
                        principalTable: "ExerciseSpace",
                        principalColumn: "ExerciseSpaceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    ActivityLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HamsterId = table.Column<int>(type: "int", nullable: true),
                    Activity = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.ActivityLogId);
                    table.ForeignKey(
                        name: "FK_ActivityLog_Hamsters_HamsterId",
                        column: x => x.HamsterId,
                        principalTable: "Hamsters",
                        principalColumn: "HamsterId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_HamsterId",
                table: "ActivityLog",
                column: "HamsterId");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_CageId",
                table: "Hamsters",
                column: "CageId");

            migrationBuilder.CreateIndex(
                name: "IX_Hamsters_ExerciseSpaceId",
                table: "Hamsters",
                column: "ExerciseSpaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLog");

            migrationBuilder.DropTable(
                name: "Hamsters");

            migrationBuilder.DropTable(
                name: "Cages");

            migrationBuilder.DropTable(
                name: "ExerciseSpace");
        }
    }
}
