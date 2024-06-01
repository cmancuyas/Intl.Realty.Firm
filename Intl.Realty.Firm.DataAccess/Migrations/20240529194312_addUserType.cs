using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intl.Realty.Firm.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addUserType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeAssignments_DocumentTypeId",
                table: "DocumentTypeAssignments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeAssignments_TransactionTypeId",
                table: "DocumentTypeAssignments",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypeAssignments_DocumentTypes_DocumentTypeId",
                table: "DocumentTypeAssignments",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypeAssignments_TransactionTypes_TransactionTypeId",
                table: "DocumentTypeAssignments",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypeAssignments_DocumentTypes_DocumentTypeId",
                table: "DocumentTypeAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypeAssignments_TransactionTypes_TransactionTypeId",
                table: "DocumentTypeAssignments");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypeAssignments_DocumentTypeId",
                table: "DocumentTypeAssignments");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypeAssignments_TransactionTypeId",
                table: "DocumentTypeAssignments");
        }
    }
}
