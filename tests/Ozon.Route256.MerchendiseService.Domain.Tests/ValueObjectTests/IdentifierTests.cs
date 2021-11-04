using System;
using Ozon.Route256.MerchandiseService.Domain.AggregateModels.ValueObjects;
using Ozon.Route256.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace Ozon.Route256.MerchendiseService.Domain.Tests.ValueObjectTests
{
    public class IdentifierTests
    {
        [Fact]
        public void CreateIdentifierSuccess()
        {
            //Arrange 
            var identifier = 10;
        
            //Act
            var result = new Identifier(10);
        
            //Assert
            Assert.Equal(identifier, result.Value);
        }

        [Fact]
        public void CreateNegativeIdentifier()
        {
            //Assert
            Assert.Throws<InvalidIdentifierException>(() => new Identifier(-10));
        }
    }
}