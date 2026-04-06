using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EcommerceSuplementos.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=db.xgzqxugihagimrtdpddb.supabase.co;" +
                "Port=5432;" +
                "Database=postgres;" +
                "Username=postgres;" +
                "Password=Wakandaforever10.;" +
                "SSL Mode=Require;" +
                "Trust Server Certificate=true"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}