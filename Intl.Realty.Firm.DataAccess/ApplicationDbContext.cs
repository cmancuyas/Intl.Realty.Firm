using Intl.Realty.Firm.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intl.Realty.Firm.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<DocumentTypeAssignment> DocumentTypeAssignments { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<IRFDeal> IRFDeals { get; set; }
        public DbSet<SaleListing> SaleListings { get; set; }
        public DbSet<SaleCoop> SaleCoops { get; set; }
        public DbSet<LeaseListing> LeaseListings { get; set; }
        public DbSet<LeaseCoop> LeaseCoops { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IRFDeal>()
                .HasMany(h => h.FileUploads)
                .WithOne(w => w.IRFDeal)
                .HasForeignKey(w => w.IRFDealId);

            modelBuilder.Entity<FileUpload>()
                .HasOne(t => t.TransactionType)
                .WithOne(f => f.FileUpload)
                .HasForeignKey<FileUpload>(f=>f.Id);
            modelBuilder.Entity<SaleListing>()
                .HasOne(t => t.TransactionType)
                .WithOne(t => t.SaleListing)
                .HasForeignKey<TransactionType>(x => x.Id);
            modelBuilder.Entity<SaleListing>()
                .HasOne(t => t.IRFDeal)
                .WithOne(t=>t.SaleListing)
                .HasForeignKey<IRFDeal>(c=>c.Id);
            modelBuilder.Entity<IRFDeal>()
                .HasOne(i=>i.TransactionType)
                .WithOne(t=>t.IRFDeal)
                .HasForeignKey<TransactionType>(i=>i.Id);
            modelBuilder.Entity<TransactionType>()
                .HasOne(i=>i.IRFDeal)
                .WithOne(t=>t.TransactionType)
                .HasForeignKey<TransactionType> (i=>i.Id);
            modelBuilder.Entity<TransactionType>()
                .HasOne(i => i.SaleListing)
                .WithOne(t => t.TransactionType)
                .HasForeignKey<SaleListing>(i => i.Id);
            modelBuilder.Entity<TransactionType>()
                .HasOne(i => i.FileUpload)
                .WithOne(t => t.TransactionType)
                .HasForeignKey<TransactionType>(i => i.Id);

        }
    }
}