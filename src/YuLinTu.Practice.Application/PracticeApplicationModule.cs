using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(
        typeof(PracticeApplicationContractsModule),
        typeof(PracticeDomainModule),
        typeof(AbpAutoMapperModule))]
    public class PracticeApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<PracticeApplicationModule>();
            });
        }
    }
}