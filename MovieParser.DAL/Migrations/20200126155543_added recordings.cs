using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieParser.DAL.Migrations
{
    public partial class addedrecordings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Actors_ActorId",
                table: "MovieActor");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieActor_Movies_MovieId1",
                table: "MovieActor");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesUserData_TvListingItems_TvListingItemId",
                table: "MoviesUserData");

            migrationBuilder.DropIndex(
                name: "IX_MoviesUserData_TvListingItemId",
                table: "MoviesUserData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieActor",
                table: "MovieActor");

            migrationBuilder.DropColumn(
                name: "IsRecorded",
                table: "MoviesUserData");

            migrationBuilder.DropColumn(
                name: "TvListingItemId",
                table: "MoviesUserData");

            migrationBuilder.RenameTable(
                name: "MovieActor",
                newName: "MovieActors");

            migrationBuilder.RenameIndex(
                name: "IX_MovieActor_MovieId1",
                table: "MovieActors",
                newName: "IX_MovieActors_MovieId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieActors",
                table: "MovieActors",
                columns: new[] { "ActorId", "MovieId" });

            migrationBuilder.CreateTable(
                name: "Recordings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<long>(nullable: true),
                    RecordedAtTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recordings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recordings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recordings_MovieId",
                table: "Recordings",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActors_Actors_ActorId",
                table: "MovieActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActors_Movies_MovieId1",
                table: "MovieActors",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieActors_Actors_ActorId",
                table: "MovieActors");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieActors_Movies_MovieId1",
                table: "MovieActors");

            migrationBuilder.DropTable(
                name: "Recordings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieActors",
                table: "MovieActors");

            migrationBuilder.RenameTable(
                name: "MovieActors",
                newName: "MovieActor");

            migrationBuilder.RenameIndex(
                name: "IX_MovieActors_MovieId1",
                table: "MovieActor",
                newName: "IX_MovieActor_MovieId1");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecorded",
                table: "MoviesUserData",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TvListingItemId",
                table: "MoviesUserData",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieActor",
                table: "MovieActor",
                columns: new[] { "ActorId", "MovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesUserData_TvListingItemId",
                table: "MoviesUserData",
                column: "TvListingItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Actors_ActorId",
                table: "MovieActor",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieActor_Movies_MovieId1",
                table: "MovieActor",
                column: "MovieId1",
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
        }
    }
}
