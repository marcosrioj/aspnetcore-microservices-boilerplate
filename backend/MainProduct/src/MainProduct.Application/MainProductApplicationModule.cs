using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Shared.Authorization;

namespace MainProduct
{
    [DependsOn(
        typeof(MainProductCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class MainProductApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SharedAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MainProductApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
