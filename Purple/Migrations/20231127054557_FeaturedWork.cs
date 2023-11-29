using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Purple.Migrations
{
    public partial class FeaturedWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeaturedWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeaturedWorkPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedWorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedWorkPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturedWorkPhotos_FeaturedWork_FeaturedWorkId",
                        column: x => x.FeaturedWorkId,
                        principalTable: "FeaturedWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedWorkPhotos_FeaturedWorkId",
                table: "FeaturedWorkPhotos",
                column: "FeaturedWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedWorkPhotos");

            migrationBuilder.DropTable(
                name: "FeaturedWork");
        }
    }
}
