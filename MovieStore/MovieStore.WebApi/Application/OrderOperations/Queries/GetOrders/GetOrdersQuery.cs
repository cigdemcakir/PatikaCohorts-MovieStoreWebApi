using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Application.OrderOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrdersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<OrderViewModel> Handle()
        {
            var orders=_dbContext.Orders
                .Include(o=>o.Movie)
                .Include(o=>o.Customer)
                .OrderBy(o=>o.CustomerId)
                .ToList();
          
            List<OrderViewModel> viewModels = _mapper.Map<List<OrderViewModel>>(orders);
           
            return viewModels;
        }
    }
}
