using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        public int ActorId { get; set; }
        public UpdateActorModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var actor=_dbContext.Actors.FirstOrDefault(a=>a.Id==ActorId);
            
            if (actor is null)
            {
                throw new InvalidOperationException("Actor Not Found.");
            }
            
            bool hasActor = _dbContext.Actors.Any(a => a.Name.ToLower().Replace(" ", "") == Model.Name.ToLower().Replace(" ", "")
            && a.Surname.ToLower().Replace(" ", "") == Model.Surname.ToLower().Replace(" ", "")
            && a.Id != ActorId);
            
            if (hasActor)
            {
                throw new InvalidOperationException("There is a registered actor with the actor's name and surname.");
            }
            
            actor.Name= !string.IsNullOrEmpty(Model.Name) ? Model.Name : actor.Name;
            actor.Surname = !string.IsNullOrEmpty(Model.Surname) ? Model.Surname:actor.Surname;

            _dbContext.SaveChanges();
        }

    }
}
