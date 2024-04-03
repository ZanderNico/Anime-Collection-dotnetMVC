using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeCollection.Migrations
{
	public partial class InitialCreate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Animes",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
					PosterImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Status = table.Column<int>(type: "int", nullable: false),
					Studio = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateAired = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Animes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Genres",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Genres", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AnimeGenres",
				columns: table => new
				{
					AnimeId = table.Column<int>(type: "int", nullable: false),
					GenreId = table.Column<int>(type: "int", nullable: false),
					Id = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AnimeGenres", x => new { x.AnimeId, x.GenreId });
					table.ForeignKey(
						name: "FK_AnimeGenres_Animes_AnimeId",
						column: x => x.AnimeId,
						principalTable: "Animes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_AnimeGenres_Genres_GenreId",
						column: x => x.GenreId,
						principalTable: "Genres",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_AnimeGenres_GenreId",
				table: "AnimeGenres",
				column: "GenreId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "AnimeGenres");

			migrationBuilder.DropTable(
				name: "Animes");

			migrationBuilder.DropTable(
				name: "Genres");
		}
	}
}
