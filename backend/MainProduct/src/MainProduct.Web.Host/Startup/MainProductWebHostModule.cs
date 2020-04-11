using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MainProduct.Configuration;

namespace MainProduct.Web.Host.Startup
{
    [DependsOn(
       typeof(MainProductWebCoreModule))]
    public class MainProductWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public MainProductWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MainProductWebHostModule).GetAssembly());
        }
    }
}
