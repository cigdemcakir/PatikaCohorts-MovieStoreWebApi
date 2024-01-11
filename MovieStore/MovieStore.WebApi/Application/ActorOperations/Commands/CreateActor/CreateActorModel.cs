using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public ICollection<Movie> Movies { get; set; }
    }

    
}
