using Manzili.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.Persistence
{
    public class ManziliDbContext : DbContext
    {
        public ManziliDbContext(DbContextOptions<ManziliDbContext> options) : base(options) { }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceOption> ServiceOptions { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all entity configurations automatically
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ManziliDbContext).Assembly);
        }
    }
}
