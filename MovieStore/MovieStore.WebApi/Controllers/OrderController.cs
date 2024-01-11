using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder;
using MovieStore.WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using MovieStore.WebApi.Application.OrderOperations.Queries.GetOrders;
using MovieStore.WebApi.Application.OrderOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
       
        public OrderController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderModel newOrder)
        {
            CreateOrderCommand command=new CreateOrderCommand(_dbContext,_mapper);
           
            command.Model = newOrder;

            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
           
            validator.ValidateAndThrow(command);

            command.Handle();
          
            return Ok();
        }
        
        [HttpGet]
        public IActionResult GetOrders()
        {
            GetOrdersQuery query=new GetOrdersQuery(_dbContext, _mapper);
            
            List<OrderViewModel> orders = query.Handle();
            
            return Ok(orders);
        }
        
        [HttpGet("{customerId}")]
        public IActionResult GetOrdersByCustomerId(int customerId)
        {
            GetOrdersByCustomerIdQuery query = new GetOrdersByCustomerIdQuery(_dbContext, _mapper);
           
            query.CustomerId = customerId;

            GetOrderByCustomerIdQueryValidator validator = new GetOrderByCustomerIdQueryValidator();
           
            validator.ValidateAndThrow(query);

            List<OrderViewModel> viewModels = query.Handle();
           
            return Ok(viewModels);
        }
        
    }
}
