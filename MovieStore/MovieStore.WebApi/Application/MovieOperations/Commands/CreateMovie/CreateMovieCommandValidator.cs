using FluentValidation;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidator:AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.DirectorId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.YearOfMovie).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
        }
    }
}
