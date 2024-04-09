using Domain.Entities;
using Microsoft.EntityFrameworkCore;

/*
 * Created by    : Jahangir Alam
 * Date created  : 30.04.2024
 * Modified by   :
 * Last modified : 
 * Reviewed by   : 
 * Date Reviewed : 
 */
namespace Infrastructure
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
        {

        }

        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}