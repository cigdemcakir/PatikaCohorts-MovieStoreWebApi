using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder;
using MovieStore.WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderByCustomerIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrderByCustomerIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidCustomerIdIsGiven_Orders_ShouldBeReturned()
        {
            // Arrange
            var movieId = _dbContext.Movies.FirstOrDefault().Id;
            var customerId = _dbContext.Customers.FirstOrDefault().Id;

            CreateOrderCommand command = new CreateOrderCommand(_dbContext, _mapper);
            
            command.Model = new CreateOrderModel()
            {
                MovieId = movieId,
                CustomerId = customerId
            };
            command.Handle();

            var orderId = _dbContext.Orders.FirstOrDefault(o => o.MovieId == movieId && o.CustomerId == customerId).Id;
            
            GetOrdersByCustomerIdQuery query = new GetOrdersByCustomerIdQuery(_dbContext, _mapper);
            
            query.CustomerId = orderId;

            // Act 
            var order = FluentActions.Invoking(() => query.Handle()).Invoke();

            // Assert 
            var registeredOrder = _dbContext.Orders.Include(o => o.Movie).Include(o => o.Customer).FirstOrDefault(o => o.Id == orderId);

            order.Should().NotBeNull();
        }

        
        [Fact]
        public void WhenInvalidCustomerIdIsGiven_NoOrders_ShouldBeReturned()
        {
            // Arrange
            GetOrdersByCustomerIdQuery query = new GetOrdersByCustomerIdQuery(_dbContext, _mapper);
           
            query.CustomerId = 999999; 

            // Act
            var orders = FluentActions.Invoking(() => query.Handle()).Invoke();

            // Assert
            orders.Should().BeEmpty();
        }
    }
}
