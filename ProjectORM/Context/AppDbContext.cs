using Microsoft.EntityFrameworkCore;
using ProjectORM.Models;
using System;

namespace ProjectORM.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(connectionString: "Server=localhost;Database=DemoDb;User Id=admin;Password=admin;TrustServerCertificate=True");

        }

    }

}
