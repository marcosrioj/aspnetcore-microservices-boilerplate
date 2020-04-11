using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MainProduct.Product.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainProduct.Product
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductInputOutputDto> Get(EntityDto input);
        Task<PagedResultDto<ProductInputOutputDto>> GetAll(PagedAndSortedResultRequestDto input);
        Task<ProductInputOutputDto> Create(ProductInputOutputDto input);
        Task<ProductInputOutputDto> Update(ProductInputOutputDto input);
        Task Delete(EntityDto input);
    }
}
