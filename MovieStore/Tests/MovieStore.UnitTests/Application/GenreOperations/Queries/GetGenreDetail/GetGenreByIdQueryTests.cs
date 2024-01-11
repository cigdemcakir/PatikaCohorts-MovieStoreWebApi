using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Genre_ShouldBeReturned()
        {
            //Arrange
            int genreId = _dbContext.Genres.FirstOrDefault().Id;
            
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
            
            query.GenreId = genreId;

            //Act 
            var genre = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert 
            var registeredGenre = _dbContext.Genres.FirstOrDefault(am => am.Id == genreId);
            
            genre.Should().NotBeNull();
            genre.Name.Should().Be(registeredGenre.Name);
        }
        
        [Fact]
        public void WhenGenreIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
            
            query.GenreId = 999999999;

            //Act & Assert 
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre Not Found.");
        }
    }
}
