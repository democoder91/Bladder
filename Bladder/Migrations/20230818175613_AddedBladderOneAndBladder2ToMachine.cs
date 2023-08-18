using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bladder.Migrations
{
    /// <inheritdoc />
    public partial class AddedBladderOneAndBladder2ToMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bladders_Machines_MachineId",
                table: "Bladders");

            migrationBuilder.DropIndex(
                name: "IX_Bladders_MachineId",
                table: "Bladders");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Bladders");

            migrationBuilder.AddColumn<int>(
                name: "BladderOneId",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BladderTwoId",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_BladderOneId",
                table: "Machines",
                column: "BladderOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_BladderTwoId",
                table: "Machines",
                column: "BladderTwoId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Bladders_BladderOneId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Bladders_BladderTwoId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_BladderOneId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_BladderTwoId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "BladderOneId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "BladderTwoId",
                table: "Machines");

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Bladders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bladders_MachineId",
                table: "Bladders",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bladders_Machines_MachineId",
                table: "Bladders",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id");
        }
    }
}
