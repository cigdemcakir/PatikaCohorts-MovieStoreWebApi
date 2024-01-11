using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeReturned()
        {
            //Arrange
            int actorId = _dbContext.Actors.FirstOrDefault().Id;
            
            GetActorByIdQuery query = new GetActorByIdQuery(_dbContext, _mapper);
            
            query.ActorId = actorId;

            //Act 
            var actor = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert 
            var registeredActor = _dbContext.Actors.FirstOrDefault(am => am.Id == actorId);
            
            actor.Should().NotBeNull();
            actor.Movies.Count.Should().Be(registeredActor.Movies.Count);
            actor.ActorFullName.Should().Be(registeredActor.Name + " " + registeredActor.Surname);
        }
        [Fact]
        public void WhenActorIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            GetActorByIdQuery query = new GetActorByIdQuery(_dbContext, _mapper);
            
            query.ActorId = 999999999;

            //Act & Assert 
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor Not Found.");
        }
    }
}
