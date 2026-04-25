using Manzili.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Data.Configurations
{
    public class PaymentProofConfiguration : IEntityTypeConfiguration<PaymentProof>
    {
        public void Configure(EntityTypeBuilder<PaymentProof> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ScreenshotUrl)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.Notes)
                .HasMaxLength(1000);

            builder.Property(p => p.IsVerified)
                .HasDefaultValue(false);

            builder.Property(p => p.SubmittedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
