using MovieStore.WebApi.Application.TokenOperations.Commands.CommandHandler;
using MovieStore.WebApi.Application.TokenOperations.Commands.RequestCommanModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public Token Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.RefreshToken == RefreshToken && c.RefreshTokenExpireDate > DateTime.Now && c.IsActive);
            
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
                throw new InvalidOperationException("No Valid Refresh Token Found.");
            }
        }
    }
}
