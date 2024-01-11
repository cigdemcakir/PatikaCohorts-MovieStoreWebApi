using FluentValidation;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Commands.DeleteActorMovie
{
    public class DeleteActorMovieCommandValidator:AbstractValidator<DeleteActorMovieCommand>
    {
        public DeleteActorMovieCommandValidator()
        {
            RuleFor(command => command.ActorMovieId).NotEmpty().GreaterThan(0);
        }
    }
}
