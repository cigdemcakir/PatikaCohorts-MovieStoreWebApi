using AutoMapper;
using MovieStore.WebApi.Application.TokenOperations.Commands.CommandHandler;
using MovieStore.WebApi.Application.TokenOperations.Commands.RequestCommanModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IMovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var customer=_dbContext.Customers.FirstOrDefault(c=>c.Email==Model.Email && c.Password==Model.Password && c.IsActive);
           
            if (customer is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(customer);
                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Username or Password is Incorrect!");
            }
        }
    }
}
