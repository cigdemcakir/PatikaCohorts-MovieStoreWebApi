namespace MovieStore.WebApi.Application.DirectorOperations.Queries.QueryViewModel
{
    public class DirectorViewModel
    {
        public string DirectorFullName { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}
