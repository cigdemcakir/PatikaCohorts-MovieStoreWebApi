using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Movie_ShouldBeCreated()
        {
            //Arrange
            CreateMovieCommand command = new CreateMovieCommand(_dbContext, _mapper);
            
            command.Model = new CreateMovieModel()
            {
                Price = 150,
                Title = "movie",
                YearOfMovie = new DateTime(1990, 12, 01),
                GenreId = _dbContext.Genres.First().Id,
                DirectorId = _dbContext.Directors.First().Id
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var moive = _dbContext.Movies.FirstOrDefault(a => a.Title == "movie");
            
            moive.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenAlreadyMovieIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var movie=_dbContext.Movies.FirstOrDefault();
            
            CreateMovieCommand command = new CreateMovieCommand(_dbContext, _mapper);
            
            command.Model = new CreateMovieModel()
            {
                Price = 150,
                Title = movie.Title,
                YearOfMovie = new DateTime(1991, 12, 01),
                GenreId = movie.GenreId,
                DirectorId = movie.DirectorId
            };

            //Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is a movie with this name and director.");
        }
        [Fact]
        public void WhenGenreOfMovieThatIsNotAvailableGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            CreateMovieCommand command = new CreateMovieCommand(_dbContext, _mapper);
            
            command.Model = new CreateMovieModel()
            {
                Price = 150,
                Title = "movie",
                YearOfMovie = new DateTime(1990, 12, 01),
                GenreId = 999999999,
                DirectorId = _dbContext.Directors.First().Id
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No Valid Movie Genre Found.");
        }
    }
}
