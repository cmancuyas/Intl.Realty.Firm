using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intl.Realty.Firm.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSaleListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "IRFDeals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "IRFDeals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LeaseCoops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalSalePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FinalClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandLordName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingCommissionPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BuyingCommissionPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ListingAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingBrokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingBrokerageFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerBrokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerBrokerageFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersLawyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersLawyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersLawyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersLawyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseCoops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaseListings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalSalePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FinalClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandLordName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingCommissionPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BuyingCommissionPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ListingAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingBrokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingBrokerageFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerBrokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerBrokerageFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersLawyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersLawyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersLawyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersLawyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseListings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleCoops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalSalePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FinalClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandLordName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingCommissionPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BuyingCommissionPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ListingAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingBrokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingBrokerageFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerBrokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerBrokerageFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersLawyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersLawyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersLawyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersLawyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleCoops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleListings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeAssignmentId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeAssignment = table.Column<int>(type: "int", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalSalePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    FinalClosingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandLordName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingCommissionPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BuyingCommissionPercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ListingAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingBrokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListingBrokerageFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerAgentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerBrokerage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerBrokerageFax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersLawyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersLawyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersLawyer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersLawyerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyersPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleListings_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleListings_TransactionTypeId",
                table: "SaleListings",
                column: "TransactionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaseCoops");

            migrationBuilder.DropTable(
                name: "LeaseListings");

            migrationBuilder.DropTable(
                name: "SaleCoops");

            migrationBuilder.DropTable(
                name: "SaleListings");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "IRFDeals");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "IRFDeals");
        }
    }
}
