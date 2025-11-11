using Microsoft.EntityFrameworkCore;
using ecomServer.Models;

namespace ecomServer.DAL;

public class ecomServerDbContext : DbContext
{
    public ecomServerDbContext(DbContextOptions<ecomServerDbContext> options): base(options)
    {

    }

    public DbSet<Product> Products {get;set;}
    public DbSet<Category> Categories { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }    

}