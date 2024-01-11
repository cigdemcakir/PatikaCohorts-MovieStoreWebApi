using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommandTests:IClassFixture<CommonTestFixture>
    {

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Order_ShouldBeCreated()
        {
            //Arrange
            var movieId = _dbContext.Movies.FirstOrDefault().Id;
            
            var customerId = _dbContext.Customers.FirstOrDefault().Id;
            
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            
            command.Model = new CreateOrderModel()
            {
                MovieId = movieId,
                CustomerId = customerId
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var order = _dbContext.Orders.FirstOrDefault(am => am.MovieId == movieId && am.CustomerId == customerId);
            
            order.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenNonCustomerIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var movieId = _dbContext.Movies.FirstOrDefault().Id;
            
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            
            command.Model = new CreateOrderModel()
            {
                MovieId = movieId,
                CustomerId = 99999999
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Customer Not Found.");
        }
        
        [Fact]
        public void WhenNonMovieIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var customerId = _dbContext.Customers.FirstOrDefault().Id;
            
            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            
            command.Model = new CreateOrderModel()
            {
                MovieId = 99999999,
                CustomerId = customerId
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie Not Found.");
        }
    }
}
