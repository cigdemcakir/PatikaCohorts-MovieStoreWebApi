using AutoMapper;
using MovieStore.WebApi.DBOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDirectorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var director = _dbContext.Directors.FirstOrDefault(d =>
            d.Name.ToLower().Replace(" ", "") == Model.Name.ToLower().Replace(" ", "") &&
            d.Surname.ToLower().Replace(" ", "") == Model.Surname.ToLower().Replace(" ", ""));
           
            if (director is not null)
            {
                throw new InvalidOperationException("There is a Director.");
            }

            director = _mapper.Map<Director>(Model);
           
            _dbContext.Directors.Add(director);
            _dbContext.SaveChanges();

        }

    }
}
