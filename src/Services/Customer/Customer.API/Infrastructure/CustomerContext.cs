using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Customer.API.Infrastructure;

public class CustomerContext : DbContext
{
    public DbSet<CustomerData> Customers { get; set; }

	public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (databaseCreator != null)
            {
                if (!databaseCreator.CanConnect()) databaseCreator.Create();
                if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerData>().HasData(
            new CustomerData { Id = 1, Name = "Artem", Email = "artem@gmail.com", MobileNumber = "89999999999"}    
        );
    }
}
