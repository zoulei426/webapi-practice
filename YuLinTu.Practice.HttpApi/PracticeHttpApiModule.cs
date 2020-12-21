using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(
        typeof(PracticeApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class PracticeHttpApiModule : AbpModule
    {
    }
}