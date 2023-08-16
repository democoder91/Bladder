using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bladder.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailNotificationBoolToBladder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExpiryNotificationSent",
                table: "Bladders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_BladderTransactions_MachineId",
                table: "BladderTransactions",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_BladderTransactions_Machines_MachineId",
                table: "BladderTransactions",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BladderTransactions_Machines_MachineId",
                table: "BladderTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BladderTransactions_MachineId",
                table: "BladderTransactions");

            migrationBuilder.DropColumn(
                name: "ExpiryNotificationSent",
                table: "Bladders");
        }
    }
}
