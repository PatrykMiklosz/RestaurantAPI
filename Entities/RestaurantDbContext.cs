using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public string _connectionSting = "Server=localhost;Database=RestaurantApiDb;Uid=root;Pwd=Programist@;";
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(eb =>

            {
                eb.Property(r => r.Name).IsRequired().HasMaxLength(50);

                eb.HasOne(r => r.Address).WithOne(a => a.Restaurant).HasForeignKey<Address>(a => a.RestaurantId);
                eb.HasMany(r => r.Dishes).WithOne(d => d.Restaurant).HasForeignKey(d => d.RestaurantId);
            });

            modelBuilder.Entity<Address>(eb =>
            {
                eb.Property(a => a.Street).IsRequired().HasMaxLength(50);
                eb.Property(a => a.City).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Dish>(eb =>
            {
                eb.Property(d => d.Name).IsRequired();
                eb.Property(eb => eb.Price).IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionSting, ServerVersion.AutoDetect(_connectionSting));
        }
    }
}