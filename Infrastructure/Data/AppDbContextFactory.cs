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
     "Host=aws-1-us-east-1.pooler.supabase.com;" +
     "Port=5432;" +
     "Database=postgres;" +
     "Username=postgres.xgzqxugihagimrtdpddb;" +
     "Password=Wakandaforever10.;" +
     "SSL Mode=Require;" +
     "Trust Server Certificate=true;" +
     "Pooling=true"
 );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}