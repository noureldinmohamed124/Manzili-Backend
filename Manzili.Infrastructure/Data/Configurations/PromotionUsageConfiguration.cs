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
    public class PromotionUsageConfiguration : IEntityTypeConfiguration<PromotionUsage>
    {
        public void Configure(EntityTypeBuilder<PromotionUsage> builder)
        {
            builder.ToTable("PromotionUsages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(x => x.User)
                .WithMany(u => u.PromotionUsages)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Promotion)
                .WithMany(p => p.PromotionUsages)
                .HasForeignKey(x => x.PromotionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
