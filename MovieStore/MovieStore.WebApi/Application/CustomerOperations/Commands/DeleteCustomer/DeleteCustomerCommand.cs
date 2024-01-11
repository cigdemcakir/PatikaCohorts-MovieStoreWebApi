using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        public int CustomerId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteCustomerCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == CustomerId);
           
            if (customer is null)
            {
                throw new InvalidOperationException("Customer Not Found.");
            }

            customer.IsActive = false;

            _dbContext.SaveChanges();
        }

    }
}
