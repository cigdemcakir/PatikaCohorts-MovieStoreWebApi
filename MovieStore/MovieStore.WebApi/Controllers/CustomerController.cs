using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.CustomerOperations.Commands.AddFavouriteGenre;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateToken;
using MovieStore.WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStore.WebApi.Application.CustomerOperations.Commands.RefreshToken;
using MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomers;
using MovieStore.WebApi.Application.CustomerOperations.Queries.QueryViewModel;
using MovieStore.WebApi.Application.TokenOperations.Commands.RequestCommanModel;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomerController(MovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        //Register
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_dbContext,_mapper);
           
            command.Model = newCustomer;
            
            CreateCustomerCommandValidator validator=new CreateCustomerCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
           
            return Ok();
        }

        //Login
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel newToken)
        {
            CreateTokenCommand command=new CreateTokenCommand(_dbContext, _mapper,_configuration);
          
            command.Model = newToken;

            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
           
            validator.ValidateAndThrow(command);

            var token = command.Handle();
           
            return token;
        }

        [HttpGet("refresh/token")]
        public ActionResult<Token> RefreshToken([FromQuery] string refreshToken)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_dbContext, _configuration);
          
            command.RefreshToken = refreshToken;

            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
           
            validator.ValidateAndThrow(command);

            var resultToken= command.Handle();
           
            return resultToken;
        }

        [Authorize]
        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomer(int customerId)
        {
            DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext);
            
            command.CustomerId = customerId;

            DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
           
            validator.ValidateAndThrow(command);

            command.Handle();
          
            return Ok();
        }

        [Authorize]
        [HttpPut("UpdateFavoriteGenre")]
        public IActionResult UpdateFavoriteGenre(int customerId, int genreId)
        {
            AddFavoriteGenreCommand command = new AddFavoriteGenreCommand(_dbContext);
           
            command.CustomerId = customerId;
            
            command.GenreId= genreId;

            AddFavoriteGenreCommandValidator validator = new AddFavoriteGenreCommandValidator();
           
            validator.ValidateAndThrow(command);

            command.Handle();
           
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCustomers()
        {
            GetCustomersQuery query = new GetCustomersQuery(_dbContext, _mapper);
          
            List<CustomerViewModel> viewModels=query.Handle();
           
            return Ok(viewModels);
        }

        [Authorize]
        [HttpGet("customerId")]
        public IActionResult GetCustomerById(int customerId)
        {
            GetCustomerByIdQuery query = new GetCustomerByIdQuery(_dbContext, _mapper);
            
            query.CustomerId = customerId;

            GetCustomerByIdQueryValidator validator = new GetCustomerByIdQueryValidator();
           
            validator.ValidateAndThrow(query);

            CustomerViewModel viewModel = query.Handle();
          
            return Ok(viewModel);
        }
    }
}
