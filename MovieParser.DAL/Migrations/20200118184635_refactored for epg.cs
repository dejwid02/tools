using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieParser.DAL.Migrations
{
    public partial class refactoredforepg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Channels");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Channels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Channels");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Movies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Channels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
