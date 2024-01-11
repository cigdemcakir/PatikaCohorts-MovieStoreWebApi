using FluentValidation;

namespace MovieStore.WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreByIdQueryValidator:AbstractValidator<GetGenreByIdQuery>
    {
        public GetGenreByIdQueryValidator()
        {
            RuleFor(query => query.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}
