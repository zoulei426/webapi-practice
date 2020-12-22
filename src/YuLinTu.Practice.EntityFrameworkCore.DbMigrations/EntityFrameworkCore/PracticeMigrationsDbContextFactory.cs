using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    public class PracticeMigrationsDbContextFactory : IDesignTimeDbContextFactory<PracticeMigrationsDbContext>
    {
        public PracticeMigrationsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PracticeMigrationsDbContext>()
                .UseNpgsql("Server=127.0.0.1;Port=5432;Database=practice;User Id=postgres;Password=123456;");

            return new PracticeMigrationsDbContext(builder.Options);
        }
    }
}