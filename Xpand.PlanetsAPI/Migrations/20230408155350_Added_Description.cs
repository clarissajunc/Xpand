using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Xpand.PlanetsAPI.Migrations
{
    /// <inheritdoc />
    public partial class Added_Description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planets_Captains_DescriptionAuthorId",
                table: "Planets");

            migrationBuilder.DropIndex(
                name: "IX_Planets_DescriptionAuthorId",
                table: "Planets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Planets");

            migrationBuilder.RenameColumn(
                name: "DescriptionAuthorId",
                table: "Planets",
                newName: "DescriptionId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Planets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descriptions_Captains_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Captains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Descriptions",
                columns: new[] { "Id", "AuthorId", "Text" },
                values: new object[,]
                {
                    { 1, 1, "While visiting this planet, the robots have uncovered various forms of life." },
                    { 2, 2, "0.2% nutrients in the soil. Unfortunately that cannot sustain life." },
                    { 3, 3, "We've found another sapient species and have engaged in communication." },
                    { 4, 4, "Just a huge floating rock" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Planets_DescriptionId",
                table: "Planets",
                column: "DescriptionId",
                unique: true,
                filter: "[DescriptionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_AuthorId",
                table: "Descriptions",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planets_Descriptions_DescriptionId",
                table: "Planets",
                column: "DescriptionId",
                principalTable: "Descriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planets_Descriptions_DescriptionId",
                table: "Planets");

            migrationBuilder.DropTable(
                name: "Descriptions");

            migrationBuilder.DropIndex(
                name: "IX_Planets_DescriptionId",
                table: "Planets");

            migrationBuilder.RenameColumn(
                name: "DescriptionId",
                table: "Planets",
                newName: "DescriptionAuthorId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Planets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Planets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "While visiting this planet, the robots have uncovered various forms of life");

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "0.2% nutrients in the soil. Unfortunately that cannot sustain life.");

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "We've found another sapient species and have engaged in communication");

            migrationBuilder.UpdateData(
                table: "Planets",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Just a huge floating rock");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_DescriptionAuthorId",
                table: "Planets",
                column: "DescriptionAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planets_Captains_DescriptionAuthorId",
                table: "Planets",
                column: "DescriptionAuthorId",
                principalTable: "Captains",
                principalColumn: "Id");
        }
    }
}
