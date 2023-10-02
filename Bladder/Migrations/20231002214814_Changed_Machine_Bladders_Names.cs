using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bladder.Migrations
{
    /// <inheritdoc />
    public partial class ChangedMachineBladdersNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Bladders_BladderOneId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Bladders_BladderTwoId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "BladderTwoId",
                table: "Machines",
                newName: "RightBladderId");

            migrationBuilder.RenameColumn(
                name: "BladderOneId",
                table: "Machines",
                newName: "LeftBladderId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_BladderTwoId",
                table: "Machines",
                newName: "IX_Machines_RightBladderId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_BladderOneId",
                table: "Machines",
                newName: "IX_Machines_LeftBladderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Bladders_LeftBladderId",
                table: "Machines",
                column: "LeftBladderId",
                principalTable: "Bladders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Bladders_RightBladderId",
                table: "Machines",
                column: "RightBladderId",
                principalTable: "Bladders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Bladders_LeftBladderId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Bladders_RightBladderId",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "RightBladderId",
                table: "Machines",
                newName: "BladderTwoId");

            migrationBuilder.RenameColumn(
                name: "LeftBladderId",
                table: "Machines",
                newName: "BladderOneId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_RightBladderId",
                table: "Machines",
                newName: "IX_Machines_BladderTwoId");

            migrationBuilder.RenameIndex(
                name: "IX_Machines_LeftBladderId",
                table: "Machines",
                newName: "IX_Machines_BladderOneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Bladders_BladderOneId",
                table: "Machines",
                column: "BladderOneId",
                principalTable: "Bladders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Bladders_BladderTwoId",
                table: "Machines",
                column: "BladderTwoId",
                principalTable: "Bladders",
                principalColumn: "Id");
        }
    }
}
