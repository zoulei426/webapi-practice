using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(typeof(PracticeEntityFrameworkCoreTestModule))]
    public class PracticeDomainTestModule : AbpModule
    {
    }
}