using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.ValueObjectTests
{
    public class EmailValueTests
    {
        [Fact]
        public void CreateEmailSuccess()
        {
            //Arrange 
            var emailString = "email@email.com";
        
            //Act
            var result = new Email(emailString);
        
            //Assert
            Assert.Equal(emailString, result.Value);
        }

        [Fact]
        public void CreateEmailNullArgument()
        {
            //Arrange 
            string emailString = null;
            
            //Assert
            Assert.Throws<InvalidEmailException>(() => new Email(emailString));
        }
        
        [Fact]
        public void CreateInvalidEmail()
        {
            //Arrange 
            var emailString = "wrong email";
            
            //Assert
            Assert.Throws<InvalidEmailException>(() => new Email(emailString));
        }
    }
}