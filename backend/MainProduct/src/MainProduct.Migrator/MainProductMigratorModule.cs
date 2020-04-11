using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MainProduct.EntityFrameworkCore;
using MainProduct.Migrator.DependencyInjection;
using Shared.Configuration;
using Shared;
using Abp.Domain.Uow;
using Abp.Dependency;

namespace MainProduct.Migrator
{
    [DependsOn(typeof(MainProductEntityFrameworkModule))]
    public class MainProductMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public MainProductMigratorModule(MainProductEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(MainProductMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.ReplaceService(typeof(IConnectionStringResolver), () =>
            {
                IocManager.Register<IConnectionStringResolver, DbContextConnectionStringResolver>(DependencyLifeStyle.Transient);
            });

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MainProductMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
