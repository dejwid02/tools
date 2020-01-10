using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieParser.DAL.Migrations
{
    public partial class CreatedChildElements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieID",
                table: "TvListingItems",
                newName: "MovieId");

            migrationBuilder.AlterColumn<long>(
                name: "MovieId",
                table: "TvListingItems",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "TvListingItems",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TvListingItemId",
                table: "MoviesUserData",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "MovieId",
                table: "MoviesUserData",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "LogsData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    LastSynchronizedDate = table.Column<DateTime>(nullable: false),
                    NoOfMoviesCreated = table.Column<int>(nullable: false),
                    NoOfTvListingItemsCreated = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TvListingItems_ChannelId",
                table: "TvListingItems",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_TvListingItems_MovieId",
                table: "TvListingItems",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesUserData_MovieId",
                table: "MoviesUserData",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesUserData_TvListingItemId",
                table: "MoviesUserData",
                column: "TvListingItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUserData_Movies_MovieId",
                table: "MoviesUserData",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesUserData_TvListingItems_TvListingItemId",
                table: "MoviesUserData",
                column: "TvListingItemId",
                principalTable: "TvListingItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TvListingItems_Channels_ChannelId",
                table: "TvListingItems",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TvListingItems_Movies_MovieId",
                table: "TvListingItems",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUserData_Movies_MovieId",
                table: "MoviesUserData");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUserData_TvListingItems_TvListingItemId",
                table: "MoviesUserData");

            migrationBuilder.DropForeignKey(
                name: "FK_TvListingItems_Channels_ChannelId",
                table: "TvListingItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TvListingItems_Movies_MovieId",
                table: "TvListingItems");

            migrationBuilder.DropTable(
                name: "LogsData");

            migrationBuilder.DropIndex(
                name: "IX_TvListingItems_ChannelId",
                table: "TvListingItems");

            migrationBuilder.DropIndex(
                name: "IX_TvListingItems_MovieId",
                table: "TvListingItems");

            migrationBuilder.DropIndex(
                name: "IX_MoviesUserData_MovieId",
                table: "MoviesUserData");

            migrationBuilder.DropIndex(
                name: "IX_MoviesUserData_TvListingItemId",
                table: "MoviesUserData");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "TvListingItems",
                newName: "MovieID");

            migrationBuilder.AlterColumn<long>(
                name: "MovieID",
                table: "TvListingItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "TvListingItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TvListingItemId",
                table: "MoviesUserData",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MovieId",
                table: "MoviesUserData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
