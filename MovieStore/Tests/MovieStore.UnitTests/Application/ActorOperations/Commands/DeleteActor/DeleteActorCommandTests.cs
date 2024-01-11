using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeDeleted()
        {
            //Arrange
            CreateActorCommand createCommand = new CreateActorCommand(_dbContext, _mapper);
            
            createCommand.Model = new CreateActorModel()
            {
                Name = "testtobedeleted",
                Surname = "testtobedeleted"
            };
            
            createCommand.Handle();

            var deletedId = _dbContext.Actors.FirstOrDefault(a => a.Name == "testtobedeleted" & a.Surname == "testtobedeleted").Id;
           
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
            
            command.ActorId = deletedId;

            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var actor = _dbContext.Actors.FirstOrDefault(am => am.Id == deletedId);
            
            actor.Should().BeNull();
        }
        
        [Fact]
        public void WhenNonActorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange(Hazırlık)
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
            
            command.ActorId = 999999999;

            //Act & Assert (Çalıştırma - Doğrulama)
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor Not Found.");
        }
        
        [Fact]
        public void WhenActorStarringInMovieGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
            
            command.ActorId = _dbContext.Actors.Include(a => a.Movies).FirstOrDefault(a => a.Movies.Count() > 0).Id;

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The actor could not be deleted as there are movies in which the actor has appeared.");
        }
    }
}
