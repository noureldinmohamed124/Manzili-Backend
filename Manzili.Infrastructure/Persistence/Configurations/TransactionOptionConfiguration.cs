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
    public class TransactionOptionConfiguration : IEntityTypeConfiguration<TransactionOption>
    {
        public void Configure(EntityTypeBuilder<TransactionOption> builder)
        {
            builder.ToTable("TransactionOptions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OptionName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(18, 2);

            builder.Property(x => x.Quantity)
                .HasDefaultValue(1);

            builder.HasOne(x => x.Transaction)
                .WithMany(t => t.TransactionOptions)
                .HasForeignKey(x => x.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
