using FluentAssertions;
using MovieStore.WebApi.Application.OrderOperations.Queries.GetOrderDetail;

namespace MovieStore.UnitTests.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int orderId)
        {
            //Arrange
            GetOrdersByCustomerIdQuery command = new GetOrdersByCustomerIdQuery(null, null);
           
            command.CustomerId = orderId;

            //Act 
            GetOrderByCustomerIdQueryValidator validator = new GetOrderByCustomerIdQueryValidator(); 
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
