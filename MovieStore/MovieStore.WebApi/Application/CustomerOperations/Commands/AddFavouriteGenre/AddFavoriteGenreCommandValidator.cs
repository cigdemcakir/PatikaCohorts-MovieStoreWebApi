using FluentValidation;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.AddFavouriteGenre
{
    public class AddFavoriteGenreCommandValidator:AbstractValidator<AddFavoriteGenreCommand>
    {
        public AddFavoriteGenreCommandValidator()
        {
            RuleFor(command => command.CustomerId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.GenreId).NotEmpty().GreaterThan(0);
        }
    }
}
