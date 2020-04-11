using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero;
using Shared;

namespace MainProduct
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(SharedCoreModule))]
    public class MainProductCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MainProductCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
        }
    }
}
