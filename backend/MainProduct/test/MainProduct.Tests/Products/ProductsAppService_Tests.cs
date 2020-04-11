using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using MainProduct.Product;
using Shouldly;
using Xunit;

namespace MainProduct.Tests.Products
{
    public class ProductAppService_Tests : MainProductTestBase
    {
        private readonly IProductAppService _productsAppService;

        public ProductAppService_Tests()
        {
            _productsAppService = Resolve<IProductAppService>();
        }

        [Fact]
        public async Task Should_Get_All_Products()
        {
            //Arrange
            var input = new PagedAndSortedResultRequestDto();

            // Act
            var output = await _productsAppService.GetAll(input);

            // Assert
            output.TotalCount.ShouldBeGreaterThanOrEqualTo(0);
        }
    }
}
