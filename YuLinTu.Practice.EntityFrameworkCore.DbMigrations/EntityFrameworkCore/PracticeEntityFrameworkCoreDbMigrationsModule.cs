using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    [DependsOn(typeof(PracticeEntityFrameworkCoreModule))]
    public class PracticeEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<PracticeMigrationsDbContext>();
        }
    }
}