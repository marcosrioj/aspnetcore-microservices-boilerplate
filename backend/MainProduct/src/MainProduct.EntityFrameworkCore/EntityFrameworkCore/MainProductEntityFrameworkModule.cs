using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using MainProduct.EntityFrameworkCore.Seed;
using Shared.Configuration;
using Shared.EntityFrameworkCore;

namespace MainProduct.EntityFrameworkCore
{
    [DependsOn(
        typeof(MainProductCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(SharedEntityFrameworkModule)
        )]
    public class MainProductEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<MainProductDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        MainProductDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        var connectionString = AppConfigurations.GetConnectionString(options.DbContextOptions.Options.ContextType.ToString());
                        MainProductDbContextConfigurer.Configure(options.DbContextOptions, connectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MainProductEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
