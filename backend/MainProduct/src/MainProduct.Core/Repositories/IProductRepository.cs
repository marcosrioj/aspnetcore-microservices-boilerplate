using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainProduct.Repositories
{
    public interface IProductRepository : IRepository<Entities.Product>
    {
    }
}
