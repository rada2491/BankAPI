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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<FavoriteAccount> FavoriteAccount { get; set; }


        /*public DbSet<UserFavoriteAccount> UserFavoriteAccount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFavoriteAccount>()
                .HasKey(o => new { o.User, o.FavoriteAccountId });
        }*/
    }
}
