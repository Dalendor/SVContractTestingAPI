using Microsoft.EntityFrameworkCore;
using SVContractTestingAPI.Models;

namespace SVContractTestingAPI.Data
{
    public class SintVincentiusContext : DbContext
    {
        public SintVincentiusContext(DbContextOptions<SintVincentiusContext> options)
            : base(options)
        {
        }

        public DbSet<Family> Families { get; set; }
        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Certificate> Certificates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data voor Families
            modelBuilder.Entity<Family>().HasData(
                new Family { Id = 1, Name = "Familie Janssens", Address = "Kerkstraat 1" },
                new Family { Id = 2, Name = "Familie Peeters", Address = "Schoolstraat 5" }
            );

            // Seed data voor FoodProducts
            modelBuilder.Entity<FoodProduct>().HasData(
                new FoodProduct { Id = 1, Name = "Pasta", Quantity = 50, ExpiryDate = DateTime.Now.AddMonths(6) },
                new FoodProduct { Id = 2, Name = "Conservenbonen", Quantity = 30, ExpiryDate = DateTime.Now.AddMonths(12) }
            );

            // Seed data voor Volunteers
            modelBuilder.Entity<Volunteer>().HasData(
                new Volunteer { Id = 1, Name = "Anna Vermeulen", Email = "anna@example.com", PhoneNumber = "0471234567" },
                new Volunteer { Id = 2, Name = "Bart De Vries", Email = "bart@example.com", PhoneNumber = "0487654321" }
            );

            // Seed data voor FamilyMembers
            modelBuilder.Entity<FamilyMember>().HasData(
                new FamilyMember { Id = 1, Name = "Jan Janssens", DateOfBirth = new DateTime(1980, 5, 15), FamilyId = 1 },
                new FamilyMember { Id = 2, Name = "Marie Janssens", DateOfBirth = new DateTime(1982, 7, 22), FamilyId = 1 },
                new FamilyMember { Id = 3, Name = "Tom Peeters", DateOfBirth = new DateTime(1975, 3, 10), FamilyId = 2 }
            );

            // Seed data voor Certificates
            modelBuilder.Entity<Certificate>().HasData(
                new Certificate { Id = 1, Title = "Voedselattest", IssueDate = DateTime.Now.AddYears(-1), ExpiryDate = DateTime.Now.AddYears(1), FamilyId = 1 },
                new Certificate { Id = 2, Title = "Pamperattest", IssueDate = DateTime.Now.AddYears(-1), ExpiryDate = DateTime.Now.AddYears(1), FamilyId = 1 },
                new Certificate { Id = 3, Title = "Voedselattest", IssueDate = DateTime.Now.AddYears(-1), ExpiryDate = DateTime.Now.AddYears(1), FamilyId = 2 }
            );

            // Relaties configureren
            modelBuilder.Entity<FamilyMember>()
                .HasOne(fm => fm.Family)
                .WithMany(f => f.Members)
                .HasForeignKey(fm => fm.FamilyId);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Family)
                .WithMany(f => f.Certificates)
                .HasForeignKey(c => c.FamilyId)
                .IsRequired(true);
        }
    }
}