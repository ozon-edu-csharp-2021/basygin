using Ozon.Route256.MerchandiseService.Domain.AggregateModels.MerchRequestAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.EmployeeAggregate;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.MerchRequestAggregateTests
{
    public class EmployeeTests
    {
        [Fact]
        public void CreateEmployeeSuccess()
        {
            //Arrange    
            var id = new Identifier(1);
            var size = Size.L;
            var email = new Email("email@email.com");

            //Act 
            var employee = new Employee(id, size, email);

            //Assert
            Assert.NotNull(employee);
            
        }
        
        [Fact]
        public void CreateEmployeeWithNullIdentifier()
        {
            //Arrange    
            Identifier id = null;
            var size = Size.L;
            var email = new Email("email@email.com");

            //Assert
            Assert.Throws<EmployeeArgumentNullException>(() =>
                new Employee(id, size, email));
        }
        
        [Fact]
        public void CreateEmployeeWithNullSize()
        {
            //Arrange    
            var id = new Identifier(1);
            Size size = null;
            var email = new Email("email@email.com");

            //Assert
            Assert.Throws<EmployeeArgumentNullException>(() =>
                new Employee(id, size, email));
        }
        
        [Fact]
        public void CreateEmployeeWithNullEmail()
        {
            //Arrange    
            var id = new Identifier(1);
            var size = Size.L;
            Email email = null;

            //Assert
            Assert.Throws<EmployeeArgumentNullException>(() =>
                new Employee(id, size, email));
        }
    }
}