using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore;
using MainProduct.Entities;

namespace MainProduct.EntityFrameworkCore
{
    public class MainProductDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Product> Products { get; set; }

        public MainProductDbContext(DbContextOptions<MainProductDbContext> options)
            : base(options)
        {

        }
    }
}
