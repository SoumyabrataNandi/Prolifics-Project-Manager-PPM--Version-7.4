using PPM.Model; // Import the System namespace for basic .NET functionality.
using Microsoft.EntityFrameworkCore; // Import the Entity Framework Core namespace for database operations.


namespace PPM.Domain
{
    public class DbContextFile : DbContext // Define a class named DbContextFile that inherits from DbContext.
    {
        ConnectionString connectionString = new();
        public DbSet<Project> Projects { get; set; } // Define a DbSet property for the Project entity.
        public DbSet<Employee> Employees { get; set; } // Define a DbSet property for the Employee entity.
        public DbSet<Role> Roles { get; set; } // Define a DbSet property for the Role entity.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(connectionString.Connection());
           // Configure the database connection to use SQL Server with a specific connection string.
        }
    }
}
