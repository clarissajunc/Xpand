using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xpand.CrewsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Humans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaptainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crews_Humans_CaptainId",
                        column: x => x.CaptainId,
                        principalTable: "Humans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Robots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CrewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Robots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Robots_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Humans",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Jonathan Smith" },
                    { 2, "Hannah Intellis" },
                    { 3, "Eva Brains" },
                    { 4, "Rick Anderson" }
                });

            migrationBuilder.InsertData(
                table: "Crews",
                columns: new[] { "Id", "CaptainId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Crew1" },
                    { 2, 2, "Crew1" },
                    { 3, 3, "Crew1" },
                    { 4, 4, "Crew1" }
                });

            migrationBuilder.InsertData(
                table: "Robots",
                columns: new[] { "Id", "CrewId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "T2011" },
                    { 2, 1, "T2020" },
                    { 3, 1, "T2031" },
                    { 4, 2, "T21" },
                    { 5, 2, "T28" },
                    { 6, 2, "T29" },
                    { 7, 3, "T201" },
                    { 8, 4, "T18" },
                    { 9, 4, "T19" },
                    { 10, 4, "T31" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crews_CaptainId",
                table: "Crews",
                column: "CaptainId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Robots_CrewId",
                table: "Robots",
                column: "CrewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Robots");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Humans");
        }
    }
}
