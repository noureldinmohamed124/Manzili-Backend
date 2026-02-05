using Manzili.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Label)
                .HasMaxLength(200);

            builder.Property(a => a.Government)
                .HasMaxLength(200);

            builder.Property(x => x.PostalCode)
                .IsRequired(false);

            builder.Property(a => a.City)
                .HasMaxLength(200);

            builder.Property(x => x.Country)
                .HasMaxLength(200)
                .HasDefaultValue("Egypt");

            builder.Property(x => x.DeliveryNotes)
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(x => x.IsDefualt)
                .HasDefaultValue(true);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
