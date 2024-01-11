using FluentValidation;

namespace MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieByIdQueryValidator:AbstractValidator<GetMovieByIdQuery>
    {
        public GetMovieByIdQueryValidator()
        {
            RuleFor(query => query.MovieId).NotNull().GreaterThan(0);
        }
    }
}
