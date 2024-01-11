using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeUpdated()
        {
            //Arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext);
            
            command.DirectorId = 1;
            
            command.Model = new UpdateDirectorModel()
            {
                Name = "directortobeupdate",
                Surname = "directortobeupdate"
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var director = _dbContext.Directors.FirstOrDefault(a => a.Name == "directortobeupdate" && a.Surname == "directortobeupdate");
           
            director.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenAlreadyDirectorIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var registeredDirector = _dbContext.Directors.Last();
            
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext);
            
            command.DirectorId = 1;
            
            command.Model = new UpdateDirectorModel()
            {
                Name = registeredDirector.Name,
                Surname = registeredDirector.Surname
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is a registered director with the given name and surname.");
        }
        
        [Fact]
        public void WhenNonDirectorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext);
            
            command.DirectorId = 999999999;
            
            command.Model = new UpdateDirectorModel()
            {
                Name = "",
                Surname = ""
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director Not Found.");
        }
    }
}
