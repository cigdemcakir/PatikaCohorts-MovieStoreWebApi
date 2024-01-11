using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.GenreOperations.Commands.CreateGenre;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeCreated()
        {
            //Arrange
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            
            command.Model = new CreateGenreModel()
            {
                Name = "genretobecreated"
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var genre = _dbContext.Genres.FirstOrDefault(a => a.Name == "genretobecreated");
            
            genre.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyGenreNameIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            
            command.Model = new CreateGenreModel()
            {
                Name = _dbContext.Genres.FirstOrDefault().Name
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre already exists.");
        }
    }
}
