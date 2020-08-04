using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class TableWatchedFilmAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchedFilm",
                columns: table => new
                {
                    FilmId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedFilm", x => new { x.UserId, x.FilmId });
                    table.ForeignKey(
                        name: "FK_WatchedFilm_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchedFilm_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedFilm_FilmId",
                table: "WatchedFilm",
                column: "FilmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchedFilm");
        }
    }
}
