using Microsoft.EntityFrameworkCore;

namespace BankApp.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<LoanDetails> LoanDetails { get; set; }
    }
}
