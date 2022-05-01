using System;
using BethanysPieShopHRM.Shared;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopHRM.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<Company> Company { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Building> Building { get; set; }
        public DbSet<FlatOwner> FlatOwner { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<PriceFactor> PriceFactor { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<BTU> BTU { get; set; }
        public DbSet<Water> Water { get; set; }
        public DbSet<Electricity> Electricity { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<AccountSummary> AccountSummary { get; set; }
        public DbSet<TotalAmountDue> TotalAmountDue { get; set; }
        //public DbSet<BarChartData> BarChartData { get; set; }
        //public DbSet<PieChartData> PieChartData { get; set; }
    }
}
