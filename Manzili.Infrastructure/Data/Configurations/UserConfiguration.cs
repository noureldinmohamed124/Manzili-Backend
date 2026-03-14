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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table name
            builder.ToTable("Users");

            // Primary key
            builder.HasKey(u => u.Id);

            // FullName
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(200);

            // Email
            builder.Property(u => u.Email)
                   .HasMaxLength(256)
                   .IsUnicode(false)
                   .IsRequired();

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            // PhoneNumber
            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(20)
                   .IsRequired(false);

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(2000)
                .IsUnicode(false);

            // PasswordHash
            builder.Property(x => x.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired();

            // Role (enum stored as int)
            builder.Property(u => u.Role)
                   .IsRequired();

            // IsBlocked
            builder.Property(u => u.IsBlocked)
                   .HasDefaultValue(false);

            // BlockedUntil
            builder.Property(u => u.BlockedUntil)
                   .IsRequired(false);

            // BlockReason
            builder.Property(u => u.BlockReason)
                   .HasMaxLength(500)
                   .IsRequired(false);

            // CreatedAt
            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // UpdatedAt
            builder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Relations

            builder.HasOne(x => x.ProviderAnalytics)
                .WithOne(p => p.Provider)
                .HasForeignKey<ProviderAnalytics>(p => p.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
