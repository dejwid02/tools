using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieParser.DAL.Migrations
{
    public partial class addedcountrycolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Movies");
        }
    }
}
