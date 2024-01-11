using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateToken;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.UnitTests.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
            _configuration = testFixture.Configuration;
        }
        [Fact]
        public void WhenValidLoginInputIsGiven_Token_ShouldBeCreated()
        {
            //Arrange
            CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper, _configuration);
            
            command.Model = new CreateTokenModel()
            {
                Email = "alicej@example.com",
                Password = "alice123"
            };

            //Act 
            var login = FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            login.RefreshToken.Should().NotBeNull();
            login.AccessToken.Should().NotBeNull();
            login.ExpireDate.Should().BeOnOrAfter(DateTime.Now);
        }
        [Fact]
        public void WhenInvalidLoginInputIsGiven_Token_ShouldBeCreated()
        {
            //Arrange
            CreateTokenCommand command = new CreateTokenCommand(_dbContext, _mapper, _configuration);
            
            command.Model = new CreateTokenModel()
            {
                Email = "WrongMail@gmail.com",
                Password = "WrongPassword"
            };

            //Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Username or Password is Incorrect!");
        }
    }
}
