using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameVault.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdditionalGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Genre", "Platform", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 100, "An open-world action-adventure game set in the kingdom of Hyrule.", "Action-Adventure", "Nintendo Switch", 2017, "The Legend of Zelda: Breath of the Wild" },
                    { 101, "An epic tale of life in America's unforgiving heartland.", "Action-Adventure", "PlayStation 5", 2018, "Red Dead Redemption 2" },
                    { 102, "A rogue-like dungeon crawler where you defy the god of the dead.", "Roguelike", "PC", 2020, "Hades" },
                    { 103, "A fantasy action RPG developed by FromSoftware and George R.R. Martin.", "Action RPG", "PC", 2022, "Elden Ring" },
                    { 104, "A challenging platformer about climbing a mountain and overcoming personal struggles.", "Platformer", "Nintendo Switch", 2018, "Celeste" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 104);
        }
    }
}
