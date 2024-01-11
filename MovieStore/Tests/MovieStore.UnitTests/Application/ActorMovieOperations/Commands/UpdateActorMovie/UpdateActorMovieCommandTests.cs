using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.ActorMovieOperations.Commands.UpdateActorMovie;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.ActorMovieOperations.Commands.UpdateActorMovie
{
    public class UpdateActorMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateActorMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_ActorMovie_ShouldBeUpdated()
        {
            //Arrange
            CreateActorCommand createActorCommand = new CreateActorCommand(_dbContext, _mapper);
            createActorCommand.Model = new CreateActorModel()
            {
                Name = "actormovie",
                Surname = "actormovie"
            };
            createActorCommand.Handle();
            
            int actorId = _dbContext.Actors.FirstOrDefault(a => a.Name == "actormovie" && a.Surname == "actormovie").Id;

            UpdateActorMovieCommand command = new UpdateActorMovieCommand(_dbContext);
            
            command.ActorMovieId = 1;
            
            command.Model = new UpdateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = 1
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(am => am.ActorId == actorId && am.MovieId == 1);
            actorMovie.Should().NotBeNull();
        }
        
        [Theory]
        [InlineData(999999999, 1, 1)]
        [InlineData(1, 999999999, 1)]
        [InlineData(1, 1, 999999999)]
        public void WhenThereAreNoGivenValues_InvalidOperationException_ShouldBeErrors(int actorMovieId,int actorId, int movieId)
        {
            //Arrange
            UpdateActorMovieCommand command = new UpdateActorMovieCommand(_dbContext);
            
            command.ActorMovieId=actorMovieId;
            
            command.Model = new UpdateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = movieId
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void WhenAlreadyActorMovieRelationshipIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault();
            
            UpdateActorMovieCommand command = new UpdateActorMovieCommand(_dbContext);
            
            command.ActorMovieId = _dbContext.ActorsMovies.FirstOrDefault(a => a.Id != actorMovie.Id).Id;
            
            command.Model = new UpdateActorMovieModel()
            {
                ActorId = actorMovie.ActorId,
                MovieId = actorMovie.MovieId
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is already an actor-film relationship.");
        }
    }
}
