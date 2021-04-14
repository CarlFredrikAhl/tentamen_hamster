using Microsoft.EntityFrameworkCore.Migrations;

namespace tentamen_hamster.Migrations
{
    public partial class database5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender1",
                table: "Hamsters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender1",
                table: "Hamsters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
