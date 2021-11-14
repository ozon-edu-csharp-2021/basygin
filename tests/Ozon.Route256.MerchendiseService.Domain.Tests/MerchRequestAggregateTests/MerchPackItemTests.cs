using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.MerchRequestAggregateTests
{
    public class MerchPackItemTests
    {
        [Fact]
        public void MerchPackItemCreatSuccess()
        {
            //Arrange
            var sku = new Sku(10);
            var quantity = new Quantity(1);
            
            // Act
            var merchPackItem = new MerchPackItem(sku, quantity);
            
            //Assert
            Assert.NotNull(merchPackItem);
            Assert.Equal(sku, merchPackItem.Sku);
            Assert.Equal(quantity, merchPackItem.Quantity);
        }
        
        [Fact]
        public void MerchPackItemCreateWithNullSku()
        {
            //Arrange
            Sku sku = null;
            var quantity = new Quantity(1);
            
            //Assert
            Assert.Throws<MerchPackItemArgumentNullException>(() =>
                new MerchPackItem(sku, quantity));
        }
        
        [Fact]
        public void MerchPackItemCreateWithNullQuantity()
        {
            //Arrange
            var sku = new Sku(10);
            Quantity quantity = null;
            
            //Assert
            Assert.Throws<MerchPackItemArgumentNullException>(() =>
                new MerchPackItem(sku, quantity));
        }
        
        
    }
}