using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_Update_ShouldBeUpdated()
        {
            //Arrange
            var movieId = _dbContext.Movies.LastOrDefault().Id;
            
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            
            command.MovieId = movieId;
            
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = "movietobeupdated",
                GenreId = _dbContext.Genres.First().Id,
                DirectorId = _dbContext.Directors.First().Id,
                IsActive=true
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var movie = _dbContext.Movies.FirstOrDefault(a => a.Title == "movietobeupdated");
            
            movie.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenAlreadyMovieIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var movie = _dbContext.Movies.FirstOrDefault();
            
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            
            command.MovieId = movie.Id;
            
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = movie.Title,
                GenreId = movie.GenreId,
                DirectorId = movie.DirectorId,
                IsActive=true
            };

            //Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is a movie with this name.");
        }
        
        [Fact]
        public void WhenNonGenreIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var movieId = _dbContext.Movies.LastOrDefault().Id;
            
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            
            command.MovieId = movieId;
            
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = "movie",
                GenreId = 999999999,
                DirectorId = _dbContext.Directors.First().Id,
                IsActive = true
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre Not Found.");
        }
        
        [Fact]
        public void WhenNonMovieIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            
            command.MovieId = 999999999;
            
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = "movie",
                GenreId = _dbContext.Genres.First().Id,
                DirectorId = _dbContext.Directors.First().Id,
                IsActive = true
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie Not Found.");
        }
        [Fact]
        public void WhenNonDirectorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var movieId = _dbContext.Movies.LastOrDefault().Id;
            
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
            
            command.MovieId = movieId;
            
            command.Model = new UpdateMovieModel()
            {
                Price = 150,
                Title = "movie",
                GenreId = _dbContext.Genres.First().Id,
                DirectorId = 999999999,
                IsActive=true
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director Not Found.");
        }
    }
}
