using FinancialBuddy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Infrastructure.Persistence
{
    public class FinancialBuddyDbContext : DbContext
    {
        public FinancialBuddyDbContext(DbContextOptions<FinancialBuddyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<UserAsset> UserAssets { get; set; }
        public DbSet<ValueAsset> ValueAssets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transfers)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Subscription>()
                .HasOne(t => t.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Goal>()
                .HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserId);

            modelBuilder.Entity<UserAsset>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAssets)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserAsset>()
                .HasOne(ua => ua.Asset)
                .WithMany()
                .HasForeignKey(ua => ua.AssetId);

        }
    }
}
