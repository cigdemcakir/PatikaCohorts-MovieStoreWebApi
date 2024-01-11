using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeCreated()
        {
            //Arrange
            CreateDirectorCommand command = new CreateDirectorCommand(_dbContext, _mapper);
            
            command.Model = new CreateDirectorModel()
            {
                Name = "directorName",
                Surname = "directorSurname"
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var director = _dbContext.Directors.FirstOrDefault(a => a.Name == "directorName" && a.Surname == "directorSurname");
            
            director.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenAlreadyDirectorIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var director = _dbContext.Directors.FirstOrDefault();
            
            CreateDirectorCommand command = new CreateDirectorCommand(_dbContext, _mapper);
            
            command.Model = new CreateDirectorModel()
            {
                Name = director.Name,
                Surname = director.Surname
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is a Director.");
        }
    }
}
