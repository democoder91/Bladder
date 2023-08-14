using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bladder.Migrations
{
    /// <inheritdoc />
    public partial class AddedFullToMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Full",
                table: "Machines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Full",
                table: "Machines");
        }
    }
}
