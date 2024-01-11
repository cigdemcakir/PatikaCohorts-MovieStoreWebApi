using FluentValidation;
using MovieStore.WebApi.Application.OrderOperations.Queries.GetOrders;

namespace MovieStore.WebApi.Application.OrderOperations.Queries.GetOrderDetail
{
    public class GetOrderByCustomerIdQueryValidator:AbstractValidator<GetOrdersByCustomerIdQuery>
    {
        public GetOrderByCustomerIdQueryValidator()
        {
            RuleFor(query => query.CustomerId).NotEmpty().GreaterThan(0);
        }
    }
}
