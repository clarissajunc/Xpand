using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xpand.PlanetsAPI.Migrations
{
    /// <inheritdoc />
    public partial class Added_Crews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CrewId",
                table: "Planets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Crew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crew", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Crew",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Crew1" },
                    { 2, "Crew2" },
                    { 3, "Crew3" },
                    { 4, "Crew4" }
                });

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 1,
                column: "CrewId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 2,
                column: "CrewId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 3,
                column: "CrewId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 4,
                column: "CrewId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 5,
                column: "CrewId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Planets_CrewId",
                table: "Planets",
                column: "CrewId",
                unique: true,
                filter: "[CrewId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Planets_Crew_CrewId",
                table: "Planets",
                column: "CrewId",
                principalTable: "Crew",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planets_Crew_CrewId",
                table: "Planets");

            migrationBuilder.DropTable(
                name: "Crew");

            migrationBuilder.DropIndex(
                name: "IX_Planets_CrewId",
                table: "Planets");

            migrationBuilder.DropColumn(
                name: "CrewId",
                table: "Planets");
        }
    }
}
