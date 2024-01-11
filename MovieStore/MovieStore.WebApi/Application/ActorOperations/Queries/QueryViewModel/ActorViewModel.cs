namespace MovieStore.WebApi.Application.ActorOperations.Queries.QueryViewModel
{
    public class ActorViewModel
    {
        public string ActorFullName { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}
