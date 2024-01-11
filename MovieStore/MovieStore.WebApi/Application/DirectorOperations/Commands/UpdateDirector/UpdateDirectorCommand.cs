using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public int DirectorId { get; set; }
        public UpdateDirectorModel Model { get; set; }

        private readonly IMovieStoreDbContext _dbContext;

        public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var director=_dbContext.Directors.FirstOrDefault(d=>d.Id==DirectorId);
           
            if (director is null)
            {
                throw new InvalidOperationException("Director Not Found.");
            }
          
            bool hasDirector = _dbContext.Directors
                .Any(d => d.Name.ToLower().Replace(" ", "") == Model.Name.ToLower().Replace(" ", "") &&
                d.Surname.ToLower().Replace(" ", "") == Model.Surname.ToLower().Replace(" ", "") &&
                d.Id != DirectorId);
          
            if (hasDirector)
            {
                throw new InvalidOperationException("There is a registered director with the given name and surname.");
            }

            director.Name = !string.IsNullOrEmpty(Model.Name) ? Model.Name : director.Name;
            director.Surname = !string.IsNullOrEmpty(Model.Surname) ? Model.Surname : director.Surname;

            _dbContext.SaveChanges();
        }

    }
}
