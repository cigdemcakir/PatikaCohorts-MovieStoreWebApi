using FluentValidation;

namespace MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerByIdQueryValidator:AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(query => query.CustomerId).NotEmpty().GreaterThan(0);
        }
    }
}
