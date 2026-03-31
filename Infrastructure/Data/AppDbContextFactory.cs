using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EcommerceSuplementos.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
      "Server=TI-ESCRITORIO-S\\SQLEXPRESS;Database=EcommerceSuplementosDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}