using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Application.CustomerOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomers
{

    public class GetCustomersQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCustomersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<CustomerViewModel> Handle()
        {
            var customers = _dbContext.Customers
                .Include(c => c.Orders)
                .Include(c => c.FavoriteGenre)
                .OrderBy(c => c.Id)
                .ToList();
            
            List<CustomerViewModel> viewModels = _mapper.Map<List<CustomerViewModel>>(customers);
           
            return viewModels;
        }
    }
}
