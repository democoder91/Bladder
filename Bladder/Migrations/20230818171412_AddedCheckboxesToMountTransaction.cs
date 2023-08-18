using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bladder.Migrations
{
    /// <inheritdoc />
    public partial class AddedCheckboxesToMountTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChangedCopperElement",
                table: "BladderTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChangedMainMandrillSeal",
                table: "BladderTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChangedPiping",
                table: "BladderTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChangedRotaryJoint",
                table: "BladderTransactions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChangedShoulderSeal",
                table: "BladderTransactions",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedCopperElement",
                table: "BladderTransactions");

            migrationBuilder.DropColumn(
                name: "ChangedMainMandrillSeal",
                table: "BladderTransactions");

            migrationBuilder.DropColumn(
                name: "ChangedPiping",
                table: "BladderTransactions");

            migrationBuilder.DropColumn(
                name: "ChangedRotaryJoint",
                table: "BladderTransactions");

            migrationBuilder.DropColumn(
                name: "ChangedShoulderSeal",
                table: "BladderTransactions");
        }
    }
}
