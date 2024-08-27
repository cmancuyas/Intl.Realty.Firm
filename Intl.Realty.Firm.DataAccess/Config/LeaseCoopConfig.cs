using Intl.Realty.Firm.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intl.Realty.Firm.DataAccess.Config
{
    public class LeaseCoopConfig : IEntityTypeConfiguration<LeaseCoop>
    {
        public void Configure(EntityTypeBuilder<LeaseCoop> builder)
        {
            builder.ToTable("LeaseCoops");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Id).UseIdentityColumn();
            builder.Property(x=>x.DealStatusId).IsRequired();

            builder.Property(n => n.IsActive).IsRequired();
            builder.Property(n => n.CreatedBy).IsRequired();
            builder.Property(n => n.CreatedAt).IsRequired();

            builder
            .HasOne(f => f.TransactionType)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            builder
            .HasOne(f => f.IRFDeal)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

            builder
            .HasOne(f => f.DealStatus)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
