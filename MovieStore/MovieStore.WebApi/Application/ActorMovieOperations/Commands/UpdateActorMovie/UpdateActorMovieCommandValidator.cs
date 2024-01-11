using FluentValidation;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Commands.UpdateActorMovie
{
    public class UpdateActorMovieCommandValidator:AbstractValidator<UpdateActorMovieCommand>
    {
        public UpdateActorMovieCommandValidator()
        {
            RuleFor(command => command.ActorMovieId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.ActorId).NotEmpty().GreaterThan(0);
            RuleFor(command => command.Model.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
