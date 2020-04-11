using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MainProduct.EntityFrameworkCore
{
    public static class MainProductDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MainProductDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MainProductDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
