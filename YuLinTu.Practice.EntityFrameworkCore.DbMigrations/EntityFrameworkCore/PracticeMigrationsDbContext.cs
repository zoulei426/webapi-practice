using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    public class PracticeMigrationsDbContext : AbpDbContext<PracticeMigrationsDbContext>
    {
        public PracticeMigrationsDbContext(DbContextOptions<PracticeMigrationsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePractice();
        }
    }
}