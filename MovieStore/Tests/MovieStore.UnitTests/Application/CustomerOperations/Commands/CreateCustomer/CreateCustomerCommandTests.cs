using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.UnitTests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateCustomerCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_User_ShouldBeCreated()
        {
            //Arrange
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            
            command.Model = new CreateCustomerModel()
            {
                Name = "newName",
                Surname = "newSurname",
                Email = "newEmail@gmail.com",
                Password = "12345",
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var customer = _dbContext.Customers.FirstOrDefault(u => u.Email == "newEmail@gmail.com");
            
            customer.Should().NotBeNull();
            customer.Id.Should().BeGreaterThan(0);
            customer.Name.Should().NotBeNull();
            customer.Surname.Should().NotBeNull();
            customer.Email.Should().NotBeNull();
            customer.Password.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenAlreadyExistEmailIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            //Arrange
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext, _mapper);
            
            command.Model = new CreateCustomerModel()
            {
                Name = "Alice",
                Surname = "Johnson",
                Email = "alicej@example.com",
                Password = "alice123"
            };
            
            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("User Already Exists.");

        }
    }
}
