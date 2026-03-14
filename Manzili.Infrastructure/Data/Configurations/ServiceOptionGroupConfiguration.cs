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
    public class ServiceOptionGroupConfiguration : IEntityTypeConfiguration<ServiceOptionGroup>
    {
        public void Configure(EntityTypeBuilder<ServiceOptionGroup> builder)
        {
            builder.ToTable("ServiceOptionGroups");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.IsRequired)
                .HasDefaultValue(false);

            builder.Property(x => x.AllowMultiple)
                .HasDefaultValue(false);

            builder.Property(x => x.DisplayOrder)
                .HasDefaultValue(0);

            builder.HasIndex(x => new { x.ServiceId, x.Name })
                .IsUnique()
                .HasDatabaseName("UX_ServiceOptionGroup_Service_Name");

            builder.HasOne(x => x.Service)
                .WithMany(s => s.OptionGroups)
                .HasForeignKey(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
