using Microsoft.EntityFrameworkCore.Migrations;

namespace tentamen_hamster.Migrations
{
    public partial class fixedicollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HamsterId",
                table: "ExerciseSpace");

            migrationBuilder.DropColumn(
                name: "HamsterId",
                table: "Cages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HamsterId",
                table: "ExerciseSpace",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HamsterId",
                table: "Cages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
