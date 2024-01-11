using FluentValidation;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidator:AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(command=>command.ActorId).NotEmpty().GreaterThan(0);
        }
    }
}
