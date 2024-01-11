using FluentValidation;

namespace MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorByIdQueryValidator:AbstractValidator<GetDirectorByIdQuery>
    {
        public GetDirectorByIdQueryValidator()
        {
            RuleFor(query => query.DirectorId).NotEmpty().GreaterThan(0);
        }
    }
}
