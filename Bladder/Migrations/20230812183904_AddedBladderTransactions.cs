using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bladder.Migrations
{
    /// <inheritdoc />
    public partial class AddedBladderTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BladderTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BladderId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BladderTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BladderTransactions_Bladders_BladderId",
                        column: x => x.BladderId,
                        principalTable: "Bladders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceFindings",
                columns: table => new
                {
                    MaintenanceTransactionId = table.Column<int>(type: "int", nullable: false),
                    FindingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceFindings", x => new { x.MaintenanceTransactionId, x.FindingId });
                    table.ForeignKey(
                        name: "FK_MaintenanceFindings_BladderTransactions_MaintenanceTransactionId",
                        column: x => x.MaintenanceTransactionId,
                        principalTable: "BladderTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceFindings_Finding_FindingId",
                        column: x => x.FindingId,
                        principalTable: "Finding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BladderTransactions_BladderId",
                table: "BladderTransactions",
                column: "BladderId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceFindings_FindingId",
                table: "MaintenanceFindings",
                column: "FindingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceFindings");

            migrationBuilder.DropTable(
                name: "BladderTransactions");
        }
    }
}
