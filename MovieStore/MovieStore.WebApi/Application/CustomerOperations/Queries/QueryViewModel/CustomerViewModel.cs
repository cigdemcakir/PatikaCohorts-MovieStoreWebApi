using MovieStore.WebApi.Application.GenreOperations.Queries.QueryViewModel;
using MovieStore.WebApi.Application.OrderOperations.Queries.QueryViewModel;

namespace MovieStore.WebApi.Application.CustomerOperations.Queries.QueryViewModel
{
    public class CustomerViewModel
    {
        public string FullName { get; set; }
      
        public ICollection<GenreViewModel> FavoriteGenre { get; set; }
      
        public ICollection<OrderViewModel> Orders { get; set; }
    }
}
