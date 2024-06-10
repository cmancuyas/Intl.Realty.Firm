using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intl.Realty.Firm.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
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
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
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
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
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
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleListings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    IRFDealId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleListings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
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
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IRFDeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUploadId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_IRFDeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IRFDeals_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    table.ForeignKey(
                        name: "FK_LeaseCoops_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_LeaseListings_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_SaleCoops_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypeAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false),
                    IRFDealId = table.Column<int>(type: "int", nullable: true),
                    LeaseCoopId = table.Column<int>(type: "int", nullable: true),
                    LeaseListingId = table.Column<int>(type: "int", nullable: true),
                    SaleCoopId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypeAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTypeAssignments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentTypeAssignments_IRFDeals_IRFDealId",
                        column: x => x.IRFDealId,
                        principalTable: "IRFDeals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentTypeAssignments_LeaseCoops_LeaseCoopId",
                        column: x => x.LeaseCoopId,
                        principalTable: "LeaseCoops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentTypeAssignments_LeaseListings_LeaseListingId",
                        column: x => x.LeaseListingId,
                        principalTable: "LeaseListings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentTypeAssignments_SaleCoops_SaleCoopId",
                        column: x => x.SaleCoopId,
                        principalTable: "SaleCoops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentTypeAssignments_TransactionTypes_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeAssignments_DocumentTypeId",
                table: "DocumentTypeAssignments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeAssignments_IRFDealId",
                table: "DocumentTypeAssignments",
                column: "IRFDealId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeAssignments_LeaseCoopId",
                table: "DocumentTypeAssignments",
                column: "LeaseCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeAssignments_LeaseListingId",
                table: "DocumentTypeAssignments",
                column: "LeaseListingId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeAssignments_SaleCoopId",
                table: "DocumentTypeAssignments",
                column: "SaleCoopId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeAssignments_TransactionTypeId",
                table: "DocumentTypeAssignments",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IRFDeals_TransactionTypeId",
                table: "IRFDeals",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseCoops_TransactionTypeId",
                table: "LeaseCoops",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseListings_TransactionTypeId",
                table: "LeaseListings",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleCoops_TransactionTypeId",
                table: "SaleCoops",
                column: "TransactionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DocumentTypeAssignments");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "SaleListings");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "IRFDeals");

            migrationBuilder.DropTable(
                name: "LeaseCoops");

            migrationBuilder.DropTable(
                name: "LeaseListings");

            migrationBuilder.DropTable(
                name: "SaleCoops");

            migrationBuilder.DropTable(
                name: "TransactionTypes");
        }
    }
}
