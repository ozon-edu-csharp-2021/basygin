using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.ValueObjectTests
{
    public class SkuValueTests
    {
        [Fact]
        public void CreateSkuSuccess()
        {
            //Arrange 
            var quantity = 10;
        
            //Act
            var result = new Quantity(10);
        
            //Assert
            Assert.Equal(quantity, result.Value);
        }
        
        [Fact]
        public void CreateNegativeSku()
        {
            //Assert
            Assert.Throws<InvalidSkuException>(() => new Sku(-10));
        }
    }
}