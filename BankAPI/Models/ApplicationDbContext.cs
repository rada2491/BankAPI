using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<FavoriteAccount> FavoriteAccount { get; set; }
        public DbSet<UserFavoriteAccount> UserFavoriteAccounts { get; set; }
        public DbSet<Payments> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payments>()
                .HasKey(bc => new
                {
                    bc.ApplicationUserId,
                    bc.ServiceId
                });

            modelBuilder.Entity<Payments>()
                .HasOne(bc => bc.ApplicationUser)
                .WithMany(b => b.Payments)
                .HasForeignKey(bc => bc.ApplicationUserId);

            modelBuilder.Entity<Payments>()
                .HasOne(bc => bc.Service)
                .WithMany(c => c.Payments)
                .HasForeignKey(bc => bc.ServiceId);

            //compose foreign for favorite

            modelBuilder.Entity<UserFavoriteAccount>()
                .HasKey(bc => new
                {
                    bc.ApplicationUserId,
                    bc.FavoriteAccountId
                });

            modelBuilder.Entity<UserFavoriteAccount>()
                .HasOne(bc => bc.ApplicationUser)
                .WithMany(b => b.FavAccounts)
                .HasForeignKey(bc => bc.ApplicationUserId);

            modelBuilder.Entity<UserFavoriteAccount>()
                .HasOne(bc => bc.FavoriteAccount)
                .WithMany(c => c.UserFavoriteAccount)
                .HasForeignKey(bc => bc.FavoriteAccountId);
        }
        
    }
}
