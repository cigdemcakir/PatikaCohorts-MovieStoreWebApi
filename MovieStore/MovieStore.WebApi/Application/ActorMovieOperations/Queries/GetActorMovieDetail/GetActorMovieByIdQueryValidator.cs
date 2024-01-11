using FluentValidation;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Queries.GetActorMovieDetail
{
    public class GetActorMovieByIdQueryValidator:AbstractValidator<GetActorMovieByIdQuery>
    {
        public GetActorMovieByIdQueryValidator()
        {
            RuleFor(query => query.ActorMovieId).NotEmpty().GreaterThan(0);
        }
    }
}
