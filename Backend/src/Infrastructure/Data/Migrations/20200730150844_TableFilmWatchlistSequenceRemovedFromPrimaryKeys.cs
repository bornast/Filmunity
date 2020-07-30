using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class TableFilmWatchlistSequenceRemovedFromPrimaryKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmWatchlist",
                table: "FilmWatchlist");

            migrationBuilder.DropIndex(
                name: "IX_FilmWatchlist_WatchlistId",
                table: "FilmWatchlist");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmWatchlist",
                table: "FilmWatchlist",
                columns: new[] { "WatchlistId", "FilmId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmWatchlist",
                table: "FilmWatchlist");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmWatchlist",
                table: "FilmWatchlist",
                columns: new[] { "Sequence", "WatchlistId", "FilmId" });

            migrationBuilder.CreateIndex(
                name: "IX_FilmWatchlist_WatchlistId",
                table: "FilmWatchlist",
                column: "WatchlistId");
        }
    }
}
