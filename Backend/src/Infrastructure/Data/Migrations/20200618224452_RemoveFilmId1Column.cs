using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class RemoveFilmId1Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Film_FilmId1",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Film_FilmId1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_FilmId1",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Rating_FilmId1",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "FilmId1",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "FilmId1",
                table: "Rating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilmId1",
                table: "Review",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilmId1",
                table: "Rating",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_FilmId1",
                table: "Review",
                column: "FilmId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_FilmId1",
                table: "Rating",
                column: "FilmId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Film_FilmId1",
                table: "Rating",
                column: "FilmId1",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Film_FilmId1",
                table: "Review",
                column: "FilmId1",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
