using Microsoft.EntityFrameworkCore;
using ecomServer.Models;

namespace ecomServer.DAL;

/// <summary>
/// The Entity Framework database context for the e-commerce system.
/// Contains DbSets for all domain entities.
/// </summary>
public class ecomServerDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ecomServerDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public ecomServerDbContext(DbContextOptions<ecomServerDbContext> options) : base(options)
    {

    }

    /// <summary>
    /// Gets or sets the products table.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets the categories table.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Gets or sets the inventories table.
    /// </summary>
    public DbSet<Inventory> Inventories { get; set; }

    /// <summary>
    /// Gets or sets the customers table.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Gets or sets the orders table.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Gets or sets the order items table.
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; }

    /// <summary>
    /// Gets or sets the users table.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the roles table.
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// Gets or sets the user-role associations table.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; }

}