using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.ActorMovieOperations.Queries.GetActorMovieDetail;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.ActorMovieOperations.Queries.GetActorMovieDetail
{
    public class GetActorMovieByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorMovieByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_ActorMovie_ShouldBeReturned()
        {
            //Arrange
            int actorMovieId = _dbContext.ActorsMovies.FirstOrDefault().Id;
            
            GetActorMovieByIdQuery query = new GetActorMovieByIdQuery(_dbContext, _mapper);
            
            query.ActorMovieId = actorMovieId;

            //Act 
            var actorMovie = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert
            var registeredActorMovie = _dbContext.ActorsMovies.FirstOrDefault(a => a.Id == actorMovieId);
            
            actorMovie.Should().NotBeNull();
            actorMovie.MovieTitle.Should().Be(registeredActorMovie.Movie.Title);
            actorMovie.ActorName.Should().Be(registeredActorMovie.Actor.Name + " " + registeredActorMovie.Actor.Surname);
        }
        [Fact]
        public void WhenActorMovieIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            GetActorMovieByIdQuery query = new GetActorMovieByIdQuery(_dbContext, _mapper);
            
            query.ActorMovieId = 999999999;

            //Act & Assert 
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No Actor-Film Relationship Found.");
        }
    }
}
