using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS.Api.Models;

namespace POS.Api.Data
{
    public class PosDbContext(DbContextOptions<PosDbContext> options)
        : IdentityDbContext<Employee>(options)
    {
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Loyalty> Loyalty { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
