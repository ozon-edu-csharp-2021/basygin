using System;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.MerchRequestAggregateTests
{
    public class MerchRequestAggregateTests
    {
        [Fact]
        public void CreateMerchRequestWithEmptyListSuccess()
        {
            //Arrange    
            var type = MerchRequestType.WelcomePack;
            var employee = new Employee( new Identifier(10), Size.L, new Email("email@email.com"));
            var createdAt = DateTime.Now;

            //Act 
            var merchRequest = new MerchRequest(type, employee, createdAt);

            //Assert
            Assert.NotNull(merchRequest);
            Assert.Equal(type, merchRequest.Type);
            Assert.Equal(employee, merchRequest.Employee);
            Assert.Equal(createdAt, merchRequest.CreatedAt);
        }
        
        [Fact]
        public void CreateMerchRequestAndAddItemWaitStatusToList()
        {
            //Arrange    
            var type = MerchRequestType.WelcomePack;
            var employee = new Employee( new Identifier(10), Size.L, new Email("email@email.com"));
            var createdAt = DateTime.Now;

            //Act 
            var merchRequest = new MerchRequest(type, employee, createdAt);
            
            merchRequest.AddItem(
                new MerchRequestItem(
                    new Identifier(123),
                    new Sku(1234),
                    new Quantity(2)
                ));

            //Assert
            Assert.Single(merchRequest.Items);
            Assert.Equal(MerchRequestStatus.Wait, merchRequest.Status);
        }
        
        [Fact]
        public void CreateMerchRequestAndAddItemDoneStatusToList()
        {
            //Arrange    
            var type = MerchRequestType.WelcomePack;
            var employee = new Employee( new Identifier(10), Size.L, new Email("email@email.com"));
            var createdAt = DateTime.Now;

            //Act 
            var merchRequest = new MerchRequest(type, employee, createdAt);
            
            merchRequest.AddItem(
                new MerchRequestItem(
                    new Identifier(123),
                    new Sku(1234),
                    new Quantity(2),
                    new IssuedQuantity(2)
                ));

            //Assert
            Assert.Single(merchRequest.Items);
            Assert.Equal(MerchRequestStatus.Done, merchRequest.Status);
        }
        
        [Fact]
        public void CreateMerchRequestAndAddNullItem()
        {
            //Arrange    
            var type = MerchRequestType.WelcomePack;
            var employee = new Employee( new Identifier(10), Size.L, new Email("email@email.com"));
            var createdAt = DateTime.Now;

            //Act 
            var merchRequest = new MerchRequest(type, employee, createdAt);

            //Assert
            Assert.Throws<MerchRequestItemArgumentNullException>(() => merchRequest.AddItem(null));
        }
        
        [Fact]
        public void CreateMerchRequestWithNullType()
        {
            //Arrange    
            MerchRequestType type = null;
            var employee = new Employee( new Identifier(10), Size.L, new Email("email@email.com"));
            var createdAt = DateTime.Now;

            //Assert
            Assert.Throws<MerchRequestItemArgumentNullException>(() => new MerchRequest(type, employee, createdAt));
        }
        
        [Fact]
        public void CreateMerchRequestWithNullEmployee()
        {
            //Arrange    
            var type = MerchRequestType.WelcomePack;
            Employee employee = null;
            var createdAt = DateTime.Now;

            //Assert
            Assert.Throws<MerchRequestItemArgumentNullException>(() => new MerchRequest(type, employee, createdAt));
        }
    }
}