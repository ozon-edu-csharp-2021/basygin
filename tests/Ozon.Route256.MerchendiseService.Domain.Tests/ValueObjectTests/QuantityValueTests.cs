using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.ValueObjectTests
{
    public class QuantityValueTests
    {
        [Fact]
        public void CreateQuantitySuccess()
        {
            //Arrange 
            var quantity = 10;
        
            //Act
            var result = new Quantity(10);
        
            //Assert
            Assert.Equal(quantity, result.Value);
        }
        
        [Fact]
        public void CreateNegativeQuantity()
        {
            //Assert
            Assert.Throws<InvalidQuantityException>(() => new Quantity(-10));
        }
        
        [Fact]
        public void CreateZeroQuantity()
        {
            //Assert
            Assert.Throws<InvalidQuantityException>(() => new Quantity(0));
        }
    }
}