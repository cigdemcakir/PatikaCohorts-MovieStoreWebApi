using FluentValidation;

namespace MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorByIdQueryValidator:AbstractValidator<GetActorByIdQuery>
    {
        public GetActorByIdQueryValidator()
        {
            RuleFor(query=>query.ActorId).NotEmpty().GreaterThan(0);
        }
    }
}
