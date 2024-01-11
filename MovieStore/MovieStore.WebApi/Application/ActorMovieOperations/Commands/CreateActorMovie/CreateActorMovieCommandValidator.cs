using FluentValidation;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Commands.CreateActorMovie
{
    public class CreateActorMovieCommandValidator:AbstractValidator<CreateActorMovieCommand>
    {
        public CreateActorMovieCommandValidator()
        {
            RuleFor(command => command.Model.ActorId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
