using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.MerchRequestAggregateTests
{
    public class MerchRequestItemTests
    {
        [Fact]
        public void CreateMerchRequestItemSuccess()
        {
            //Arrange    
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            var quantity = new Quantity(2);

            //Act 
            var merchRequestItem = new MerchRequestItem(merchRequestId, sku, quantity);

            //Assert
            Assert.NotNull(merchRequestItem);
            Assert.Equal(merchRequestId, merchRequestItem.MerchRequestId);
            Assert.Equal(sku, merchRequestItem.Sku);
            Assert.Equal(quantity, merchRequestItem.Quantity);
            Assert.Equal(new IssuedQuantity(0), merchRequestItem.IssuedQuantity);
        }

        [Fact]
        public void CreateMerchRequestItemWithIssuedQuantitySuccess()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            var quantity = new Quantity(2);
            var issuedQuantity = new IssuedQuantity(2);

            //Act 
            var merchRequestItem = new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity);

            //Assert
            Assert.NotNull(merchRequestItem);
            Assert.Equal(merchRequestId, merchRequestItem.MerchRequestId);
            Assert.Equal(sku, merchRequestItem.Sku);
            Assert.Equal(quantity, merchRequestItem.Quantity);
            Assert.Equal(issuedQuantity, merchRequestItem.IssuedQuantity);
        }

        [Fact]
        public void MerchRequestItemWithIssuedQuantityMoreThanRequired()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            var quantity = new Quantity(2);
            var issuedQuantity = new IssuedQuantity(5);

            //Assert
            Assert.Throws<InvalidMerchRequestItemIssuedQuantityException>(() =>
                new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity));
        }
        
        [Fact]
        public void MerchRequestItemIncreaseIssuedQuantityMoreThanRequired()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            var quantity = new Quantity(3);
            var issuedQuantity = new IssuedQuantity(2);

            var valueToIncrease = 5;
            
            // Act
            var merchRequestItem = new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity);
            
            //Assert
            Assert.Throws<InvalidMerchRequestItemIssuedQuantityException>(() =>
                merchRequestItem.IncreaseIssuedQuantity(valueToIncrease));
        }
        
        [Fact]
        public void MerchRequestItemNewStatus()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            var quantity = new Quantity(3);

            var status = MerchRequestItemStatus.New;
            
            // Act
            var merchRequestItem = new MerchRequestItem(merchRequestId, sku, quantity);
            
            //Assert
            Assert.Equal(merchRequestItem.MerchRequestItemStatus, status);
        }
        
        [Fact]
        public void MerchRequestItemDoneStatusWhenCreation()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            var quantity = new Quantity(3);
            var issuedQuantity = new IssuedQuantity(3);

            var status = MerchRequestItemStatus.Done;
            
            // Act
            var merchRequestItem = new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity);
            
            //Assert
            Assert.Equal(merchRequestItem.MerchRequestItemStatus, status);
        }
        
        [Fact]
        public void MerchRequestItemDoneStatusWhenIncreaseIssuedQuantity()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            var quantity = new Quantity(3);
            var issuedQuantity = new IssuedQuantity(1);

            var valueToIncrease = 2;
            var status = MerchRequestItemStatus.Done;
            
            // Act
            var merchRequestItem = new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity);
            merchRequestItem.IncreaseIssuedQuantity(valueToIncrease);
            
            //Assert
            Assert.Equal(merchRequestItem.MerchRequestItemStatus, status);
        }
        
        [Fact]
        public void MerchRequestItemCreateWithNullIdentifier()
        {
            //Arrange
            Identifier merchRequestId = null;
            var sku = new Sku(10);
            var quantity = new Quantity(3);
            var issuedQuantity = new IssuedQuantity(1);
            
            //Assert
            Assert.Throws<MerchRequestItemArgumentNullException>(() =>
                new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity));
        }
        
        [Fact]
        public void MerchRequestItemCreateWithNullSku()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            Sku sku = null;
            var quantity = new Quantity(3);
            var issuedQuantity = new IssuedQuantity(1);
            
            //Assert
            Assert.Throws<MerchRequestItemArgumentNullException>(() =>
                new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity));
        }
        
        [Fact]
        public void MerchRequestItemCreateWithNullQuantity()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            Quantity quantity = null;
            var issuedQuantity = new IssuedQuantity(1);
            
            //Assert
            Assert.Throws<MerchRequestItemArgumentNullException>(() =>
                new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity));
        }
        
        [Fact]
        public void MerchRequestItemCreateWithNullIssuedQuantity()
        {
            //Arrange
            var merchRequestId = new Identifier(1);
            var sku = new Sku(10);
            var quantity = new Quantity(3);
            IssuedQuantity issuedQuantity = null;
            
            //Assert
            Assert.Throws<MerchRequestItemArgumentNullException>(() =>
                new MerchRequestItem(merchRequestId, sku, quantity, issuedQuantity));
        }
    }
}