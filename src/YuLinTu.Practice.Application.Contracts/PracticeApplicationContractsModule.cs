using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(typeof(PracticeDomainSharedModule))]
    public class PracticeApplicationContractsModule : AbpModule
    {
    }
}