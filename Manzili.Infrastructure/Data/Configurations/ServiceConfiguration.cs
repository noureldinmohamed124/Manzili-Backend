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
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasKey(x => x.Id);


            // -----------------------------
            // Columns
            // -----------------------------

            builder.Property(x => x.Title)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.ServiceDescription)
                .HasMaxLength(5000);

            builder.Property(x => x.BasePrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.ViewsCount)
                .HasDefaultValue(0);

            builder.Property(x => x.TotalPurchases)
                .HasDefaultValue(0);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");


            // -----------------------------
            // Indexes for Filtering & Sorting
            // -----------------------------
            builder.HasIndex(s => s.ViewsCount)
                .HasDatabaseName("IX_Service_ViewsCount");

            builder.HasIndex(s => s.TotalPurchases)
                .HasDatabaseName("IX_Service_TotalPurchases");

            // Composite Index
            builder.HasIndex(s => new { s.StatusId, s.IsRecommended, s.CategoryId })
                .HasDatabaseName("IX_Service_Status_Recommended_Category");

            builder.HasIndex(s => s.CreatedAt)
                .HasDatabaseName("IX_Service_CreatedAt");

            builder.HasIndex(s => s.IsRecommended)
                .HasDatabaseName("IX_Service_IsRecommended");

            // Optional: Denormalized flag for active promotions
            builder.Property(s => s.HasActivePromotion)
                .HasDefaultValue(false);
            builder.HasIndex(s => s.HasActivePromotion)
                .HasDatabaseName("IX_Service_HasActivePromotion");

            // -----------------------------
            // Relationships
            // -----------------------------
            builder.HasOne(x => x.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Provider)
                .WithMany(u => u.Services)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Status)
                .WithMany(s => s.Services)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // -----------------------------
            // Navigation Collections
            // -----------------------------
            builder.HasMany(s => s.Transactions)
                .WithOne(t => t.Service)
                .HasForeignKey(t => t.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Promotions)
                .WithOne(p => p.Service)
                .HasForeignKey(p => p.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);


            //builder.HasOne(x => x.Category)
            //    .WithMany(c => c.Services)
            //    .HasForeignKey(x => x.CategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.Provider)
            //    .WithMany(u => u.Services)
            //    .HasForeignKey(x => x.ProviderId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.Status)
            //    .WithMany(s => s.Services)
            //    .HasForeignKey(x => x.StatusId)
            //    .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
