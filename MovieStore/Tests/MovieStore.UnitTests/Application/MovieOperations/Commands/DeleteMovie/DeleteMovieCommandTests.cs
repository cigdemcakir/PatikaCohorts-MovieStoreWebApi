using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Movie_ShouldBeDeleted()
        {
            //Arrange
            CreateMovieCommand createCommand = new CreateMovieCommand(_dbContext, _mapper);
            
            createCommand.Model = new CreateMovieModel()
            {
                Title = "testtobedeleted",
                Price = 100,
                GenreId = _dbContext.Genres.FirstOrDefault().Id,
                DirectorId = _dbContext.Directors.FirstOrDefault().Id,
                YearOfMovie = new DateTime(1990, 10, 01)
            };
            
            createCommand.Handle();
            
            var deletedId = _dbContext.Movies.FirstOrDefault(a => a.Title == "testtobedeleted").Id;
            
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            
            command.MovieId = deletedId;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var movie = _dbContext.Movies.FirstOrDefault(am => am.Id == deletedId);
            
            movie.IsActive.Should().BeFalse();
        }
        [Fact]
        public void WhenNonMovieIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
            
            command.MovieId = 999999999;

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie Not Found.");
        }
    }
}
