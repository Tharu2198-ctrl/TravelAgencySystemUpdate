using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TravelAgencySystem.Models;

namespace TravelAgencySystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Booking> Bookings { get; set; }

     //   public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasKey(p => p.UserId);

            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("Role")
                .HasValue<Customer>("Customer")
                .HasValue<Admin>("Admin");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.UserId);

            base.OnModelCreating(modelBuilder);
        }

       //  public DbSet<Admin> Admins { get; set; }
    }
}