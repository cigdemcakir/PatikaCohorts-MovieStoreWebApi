using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.GenreOperations.Commands.UpdateGenre;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeUpdated()
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            
            command.GenreId = 1;
            
            command.Model = new UpdateGenreModel()
            {
                Name = "genre",
                IsActive = true
            };

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var actor = _dbContext.Genres.FirstOrDefault(am => am.Name == "genre" );
            actor.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenAlreadyGenreIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var registeredGenre = _dbContext.Genres.Last();
            
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            
            command.GenreId = _dbContext.Genres.FirstOrDefault(g=>g.Id!=registeredGenre.Id).Id;
            
            command.Model = new UpdateGenreModel()
            {
                Name = registeredGenre.Name,
                IsActive = true
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The given genre name exists in another film genre.");
        }
        
        [Fact]
        public void WhenNonGenreIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            
            command.GenreId = 999999999;
            
            command.Model = new UpdateGenreModel()
            {
                Name = "",
                IsActive = true
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre Not Found.");
        }
        
        [Fact]
        public void WhenMovieInTheGenreIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            
            command.GenreId = _dbContext.Movies.FirstOrDefault().GenreId;
            
            command.Model = new UpdateGenreModel()
            {
                Name = "genretobeupdated",
                IsActive = false
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The status cannot be set to inactive as there are movies associated with the genre.");
        }
    }
}
