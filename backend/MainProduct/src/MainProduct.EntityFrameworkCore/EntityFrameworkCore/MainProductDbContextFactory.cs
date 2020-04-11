using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Configuration;
using System;

namespace MainProduct.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MainProductDbContextFactory : IDesignTimeDbContextFactory<MainProductDbContext>
    {
        public MainProductDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MainProductDbContext>();
            var configuration = AppConfigurations.Get(AppDomain.CurrentDomain.BaseDirectory);

            MainProductDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SharedConsts.MainProductConnectionStringName));

            return new MainProductDbContext(builder.Options);
        }
    }
}
