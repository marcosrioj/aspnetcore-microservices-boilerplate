using Abp.EntityFrameworkCore;
using MainProduct.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainProduct.EntityFrameworkCore.Repositories
{
    public class ProductRepository : MainProductRepositoryBase<Entities.Product>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<MainProductDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}
