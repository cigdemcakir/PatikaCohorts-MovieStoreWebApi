using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorByIdQueryTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeReturned()
        {
            //Arrange
            int directorId = _dbContext.Directors.FirstOrDefault().Id;
            
            GetDirectorByIdQuery query = new GetDirectorByIdQuery(_dbContext, _mapper);
            
            query.DirectorId = directorId;

            //Act 
            var director = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert 
            var registeredDirector = _dbContext.Directors.FirstOrDefault(am => am.Id == directorId);
            
            director.Should().NotBeNull();
            director.Movies.Count.Should().Be(registeredDirector.Movies.Count);
            director.DirectorFullName.Should().Be(registeredDirector.Name + " " + registeredDirector.Surname);
        }
        
        [Fact]
        public void WhenDirectorIdValueIsNotFound_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            GetDirectorByIdQuery query = new GetDirectorByIdQuery(_dbContext, _mapper);
            
            query.DirectorId = 999999999;

            //Act & Assert 
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director Not Found.");
        }
    }
}
