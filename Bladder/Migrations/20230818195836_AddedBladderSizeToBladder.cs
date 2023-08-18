using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bladder.Migrations
{
    /// <inheritdoc />
    public partial class AddedBladderSizeToBladder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BladderSizeId",
                table: "Bladders",
                type: "int",
                nullable: false,
                defaultValue: 7);

            migrationBuilder.CreateIndex(
                name: "IX_Bladders_BladderSizeId",
                table: "Bladders",
                column: "BladderSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bladders_BladderSizes_BladderSizeId",
                table: "Bladders",
                column: "BladderSizeId",
                principalTable: "BladderSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bladders_BladderSizes_BladderSizeId",
                table: "Bladders");

            migrationBuilder.DropIndex(
                name: "IX_Bladders_BladderSizeId",
                table: "Bladders");

            migrationBuilder.DropColumn(
                name: "BladderSizeId",
                table: "Bladders");
        }
    }
}
