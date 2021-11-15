using Ozon.Route256.MerchandiseService.Domain.AggregateModels.EmployeeAggregate;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.MerchRequestAggregateTests
{
    public class EmployeeTests
    {
        [Fact]
        public void CreateEmployeeSuccess()
        {
            //Arrange
            var employeeId = 10;
            var size = Size.L;
            var email = new Email("email@email.com");

            //Act 
            var employee = new Employee(employeeId, size, email);

            //Assert
            Assert.NotNull(employee);
        }
        
        [Fact]
        public void CreateEmployeeWithNullSize()
        {
            //Arrange
            var employeeId = 10;
            Size size = null;
            var email = new Email("email@email.com");

            //Assert
            Assert.Throws<EmployeeArgumentNullException>(() =>
                new Employee(employeeId, size, email));
        }
        
        [Fact]
        public void CreateEmployeeWithNullEmail()
        {
            //Arrange
            var employeeId = 10;
            var size = Size.L;
            Email email = null;

            //Assert
            Assert.Throws<EmployeeArgumentNullException>(() =>
                new Employee(employeeId, size, email));
        }
    }
}