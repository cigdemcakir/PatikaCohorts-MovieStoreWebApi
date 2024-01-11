using AutoMapper;
using MovieStore.WebApi.DBOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommand
    {
        public CreateCustomerModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCustomerCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == Model.Email);
           
            if (customer is not null)
            {
                throw new InvalidOperationException("User Already Exists.");
            }
           
            customer = _mapper.Map<Customer>(Model);

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

    }
}
