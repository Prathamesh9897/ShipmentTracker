using Microsoft.EntityFrameworkCore;
using ShipmentApp.Models;

namespace ShipmentApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Ship> Ships { get; set; }
}
