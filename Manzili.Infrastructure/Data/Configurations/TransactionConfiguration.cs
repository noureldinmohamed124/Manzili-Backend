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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomRequestText)
                .HasMaxLength(2000);

            builder.Property(x => x.CustomRequestImage)
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(x => x.RawPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.CashDiscount)
                .HasPrecision(18, 2);

            builder.Property(x => x.TotalPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(x => x.Buyer)
                .WithMany(u => u.Transactions)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Provider)
                .WithMany()
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.TransactionType)
                .WithMany(t => t.Transactions)
                .HasForeignKey(x => x.TransactionTypeId);

            builder.HasOne(t => t.Service)
                .WithMany(s => s.Transactions)
                .HasForeignKey(t => t.ServiceId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.ParentTransaction)
                .WithMany()
                .HasForeignKey(x => x.ParentTransactionId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
