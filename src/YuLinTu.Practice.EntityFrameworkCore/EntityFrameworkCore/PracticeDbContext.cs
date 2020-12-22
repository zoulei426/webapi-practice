using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YuLinTu.Practice.Books;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    public class PracticeDbContext : AbpDbContext<PracticeDbContext>
    {
        public DbSet<Book> Books { get; set; }

        public PracticeDbContext(DbContextOptions<PracticeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePractice();
        }
    }
}