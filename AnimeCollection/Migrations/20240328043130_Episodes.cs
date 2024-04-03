using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeCollection.Migrations
{
	public partial class Episodes : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "Episodes",
				table: "Animes",
				type: "int",
				nullable: false,
				defaultValue: 0);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Episodes",
				table: "Animes");
		}
	}
}
