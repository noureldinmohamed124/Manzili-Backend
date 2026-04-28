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
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.ToTable("TransactionTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TransactionTypeName)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(x => x.TransactionTypeName)
                .IsUnique();

            builder.Property(x => x.BalanceSign)
                .IsRequired();

            builder.Property(x => x.StockSign)
                .IsRequired(false);

        }
    }
}
