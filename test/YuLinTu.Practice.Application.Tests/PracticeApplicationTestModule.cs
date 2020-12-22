using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(
       typeof(PracticeApplicationModule),
       typeof(PracticeDomainTestModule)
       )]
    public class PracticeApplicationTestModule : AbpModule
    {
    }
}