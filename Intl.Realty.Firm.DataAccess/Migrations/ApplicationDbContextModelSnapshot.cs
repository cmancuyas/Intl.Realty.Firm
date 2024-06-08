﻿// <auto-generated />
using System;
using Intl.Realty.Firm.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Intl.Realty.Firm.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.DocumentTypeAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("LeaseCoopId")
                        .HasColumnType("int");

                    b.Property<int?>("LeaseListingId")
                        .HasColumnType("int");

                    b.Property<int?>("SaleCoopId")
                        .HasColumnType("int");

                    b.Property<int?>("SaleListingId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("LeaseCoopId");

                    b.HasIndex("LeaseListingId");

                    b.HasIndex("SaleCoopId");

                    b.HasIndex("SaleListingId");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("DocumentTypeAssignments");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.IRFDeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MediaId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("IRFDeals");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.LeaseCoop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuyerAgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerBrokerage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerBrokerageFax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersLawyer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersLawyerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BuyingCommissionPercentage")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("DepositDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinalClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FinalSalePrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LandLordName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingAgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingBrokerage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingBrokerageFax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ListingCommissionPercentage")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("PropertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersLawyer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersLawyerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("LeaseCoops");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.LeaseListing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuyerAgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerBrokerage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerBrokerageFax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersLawyer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersLawyerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BuyingCommissionPercentage")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("DepositDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinalClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FinalSalePrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LandLordName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingAgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingBrokerage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingBrokerageFax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ListingCommissionPercentage")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("PropertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersLawyer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersLawyerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("LeaseListings");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.SaleCoop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuyerAgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerBrokerage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerBrokerageFax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersLawyer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersLawyerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BuyingCommissionPercentage")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("DepositDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinalClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FinalSalePrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LandLordName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingAgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingBrokerage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingBrokerageFax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ListingCommissionPercentage")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("PropertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersLawyer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersLawyerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("SaleCoops");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.SaleListing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuyerAgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerBrokerage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerBrokerageFax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersLawyer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersLawyerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BuyersPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BuyingCommissionPercentage")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<decimal>("DepositAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("DepositDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinalClosingDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FinalSalePrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LandLordName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingAgentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingBrokerage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListingBrokerageFax")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ListingCommissionPercentage")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("PropertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersLawyer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersLawyerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellersPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionTypeId");

                    b.ToTable("SaleListings");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.DocumentTypeAssignment", b =>
                {
                    b.HasOne("Intl.Realty.Firm.Models.Models.DocumentType", "DocumentType")
                        .WithMany()
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intl.Realty.Firm.Models.Models.LeaseCoop", null)
                        .WithMany("DocumentTypeAssignmentList")
                        .HasForeignKey("LeaseCoopId");

                    b.HasOne("Intl.Realty.Firm.Models.Models.LeaseListing", null)
                        .WithMany("DocumentTypeAssignmentList")
                        .HasForeignKey("LeaseListingId");

                    b.HasOne("Intl.Realty.Firm.Models.Models.SaleCoop", null)
                        .WithMany("DocumentTypeAssignmentList")
                        .HasForeignKey("SaleCoopId");

                    b.HasOne("Intl.Realty.Firm.Models.Models.SaleListing", null)
                        .WithMany("DocumentTypeAssignmentList")
                        .HasForeignKey("SaleListingId");

                    b.HasOne("Intl.Realty.Firm.Models.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.LeaseCoop", b =>
                {
                    b.HasOne("Intl.Realty.Firm.Models.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.LeaseListing", b =>
                {
                    b.HasOne("Intl.Realty.Firm.Models.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.SaleCoop", b =>
                {
                    b.HasOne("Intl.Realty.Firm.Models.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.SaleListing", b =>
                {
                    b.HasOne("Intl.Realty.Firm.Models.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.LeaseCoop", b =>
                {
                    b.Navigation("DocumentTypeAssignmentList");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.LeaseListing", b =>
                {
                    b.Navigation("DocumentTypeAssignmentList");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.SaleCoop", b =>
                {
                    b.Navigation("DocumentTypeAssignmentList");
                });

            modelBuilder.Entity("Intl.Realty.Firm.Models.Models.SaleListing", b =>
                {
                    b.Navigation("DocumentTypeAssignmentList");
                });
#pragma warning restore 612, 618
        }
    }
}
