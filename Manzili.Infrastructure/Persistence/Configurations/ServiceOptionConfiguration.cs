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
    public class ServiceOptionConfiguration : IEntityTypeConfiguration<ServiceOption>
    {
        public void Configure(EntityTypeBuilder<ServiceOption> builder)
        {
            builder.ToTable("ServiceOptions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ServiceOptionName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.PriceAdjustment)
                .HasPrecision(18, 2);

            builder.Property(x => x.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(x => new { x.OptionGroupId, x.ServiceOptionName })
                    .IsUnique()
                    .HasDatabaseName("UX_ServiceOption_Group_Name");

            builder.HasOne(x => x.OptionGroup)
                .WithMany(g => g.Options)
                .HasForeignKey(x => x.OptionGroupId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
