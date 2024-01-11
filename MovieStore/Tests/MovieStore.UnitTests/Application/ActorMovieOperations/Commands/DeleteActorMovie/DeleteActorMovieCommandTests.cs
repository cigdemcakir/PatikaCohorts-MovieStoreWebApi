using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.ActorMovieOperations.Commands.DeleteActorMovie;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.ActorMovieOperations.Commands.DeleteActorMovie
{
    public class DeleteActorMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteActorMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        [Fact]
        public void WhenValidInputIsGiven_ActorMovie_ShouldBeDeleted()
        {
            //Arrange
            var actorMovieId = _dbContext.ActorsMovies.Last().Id;
            
            DeleteActorMovieCommand command = new DeleteActorMovieCommand(_dbContext);
            
            command.ActorMovieId = actorMovieId;

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(a => a.Id == actorMovieId);
            
            actorMovie.Should().BeNull();
        }
        [Fact]
        public void WhenActorMovieIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {

            //Arrange
            DeleteActorMovieCommand command = new DeleteActorMovieCommand(_dbContext);
            
            command.ActorMovieId = 999999999;

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No Actor-Film Relationship Found.");
        }
    }
}
