using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.GenreOperations.Commands.CreateGenre;
using MovieStore.WebApi.Application.GenreOperations.Commands.DeleteGenre;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests:IClassFixture<CommonTestFixture>
    {

        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeDeleted()
        {
            //Arrange
            CreateGenreCommand createCommand = new CreateGenreCommand(_dbContext, _mapper);
            
            createCommand.Model = new CreateGenreModel()
            {
                Name = "testtobedeleted"
            };
            
            createCommand.Handle();
            
            var deletedId = _dbContext.Genres.FirstOrDefault(a => a.Name == "testtobedeleted").Id;
            
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            
            command.GenreId = deletedId;

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var genre = _dbContext.Genres.FirstOrDefault(am => am.Id == deletedId);
            
            genre.Should().BeNull();
        }
        
        [Fact]
        public void WhenNonGenreIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            
            command.GenreId = 999999999;

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre Not Found.");
        }
        [Fact]
        public void WhenMovieOfTheGenreIsFound_InvalidOperationException_ShouldBeError()
        {
            // Arrange
            var genreWithMovies = _dbContext.Genres
                .FirstOrDefault(g => _dbContext.Movies.Any(m => m.GenreId == g.Id));
            
            if (genreWithMovies == null)
            {
                Assert.Fail("No suitable genre was found for testing.");
            }

            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            
            command.GenreId = genreWithMovies.Id;

            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The genre could not be deleted as there are movies associated with this genre.");
        }
    }
}
