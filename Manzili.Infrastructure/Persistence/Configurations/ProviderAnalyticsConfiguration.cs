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
    public class ProviderAnalyticsConfiguration : IEntityTypeConfiguration<ProviderAnalytics>
    {
        public void Configure(EntityTypeBuilder<ProviderAnalytics> builder)
        {
            builder.ToTable("ProviderAnalytics");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalRevenue)
                .HasPrecision(18, 2);

            builder.Property(x => x.AverageRating)
                .HasPrecision(5, 2);

            builder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(x => x.Provider)
                .WithOne(u => u.ProviderAnalytics)
                .HasForeignKey<ProviderAnalytics>(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
