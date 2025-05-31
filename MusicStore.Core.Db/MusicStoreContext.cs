//using Microsoft.EntityFrameworkCore;
using MusicStore.Core.Data;

namespace MusicStore.Core.Db;

public class MusicStoreContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public MusicStoreContext(DbContextOptions<MusicStoreContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());

    }
}