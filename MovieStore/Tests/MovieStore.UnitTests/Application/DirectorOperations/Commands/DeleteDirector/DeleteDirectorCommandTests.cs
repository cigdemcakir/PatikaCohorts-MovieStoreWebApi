using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeDeleted()
        {
            //Arrange
            CreateDirectorCommand createCommand = new CreateDirectorCommand(_dbContext, _mapper);
            
            createCommand.Model = new CreateDirectorModel()
            {
                Name = "testtobedeleted",
                Surname = "testtobedeleted"
            };
            
            createCommand.Handle();
            
            var deletedId = _dbContext.Directors.FirstOrDefault(a => a.Name == "testtobedeleted" & a.Surname == "testtobedeleted").Id;
           
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
            
            command.DirectorId = deletedId;

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var director = _dbContext.Directors.FirstOrDefault(a => a.Id == deletedId);
           
            director.Should().BeNull();
        }
        
        [Fact]
        public void WhenNonDirectorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
            
            command.DirectorId = 999999999;

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director Not Found.");
        }
        [Fact]
        public void WhenActorStarringInMovieGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
            
            command.DirectorId = _dbContext.Directors.Include(a => a.Movies).FirstOrDefault(a => a.Movies.Count() > 0).Id;

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The director could not be deleted as there are movies associated with the director.");
        }
    }
}
