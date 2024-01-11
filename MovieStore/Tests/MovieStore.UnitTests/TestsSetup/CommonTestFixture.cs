using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieStore.UnitTests.Extensions;
using MovieStore.WebApi.Common;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.UnitTests.TestsSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set; }
        public CommonTestFixture()
        {
            var inMemorySettings= new Dictionary<string, string>
            {
                {"Token:Issuer", "www.MovieStore.com"},
                {"Token:Audience", "www.MovieStore.com"},
                {"Token:SecurityKey", "This is my private secret key that I use for authentication in the moviestore"},
            };
            
            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDb").Options;
            Context=new MovieStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.Initialize();

            Mapper = new MapperConfiguration(configure: cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();
        }
    }
}
