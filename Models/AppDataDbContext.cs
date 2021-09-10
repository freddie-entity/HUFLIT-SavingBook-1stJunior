using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace test.Models
{
    public class AppDataDbContext : IdentityDbContext<AppUser> 
    {
        public AppDataDbContext(DbContextOptions<AppDataDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<DetailReport> DetailReports { get; set; }
        public DbSet<DetailInterest> DetailInterests { get; set; }
        public DbSet<SavingBook> SavingBooks { get; set; }
        public DbSet<DepositPaper> DepositPapers { get; set; }
        public DbSet<WithdrawalPaper> WithdrawalPapers { get; set; }
        public object Configuration { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);//+ eliminate the default ASPNET prefix in each original table 
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
