using FluentValidation;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidator:AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(command=>command.ActorId).NotNull().GreaterThan(0);
            RuleFor(command => command.Model.Name).NotNull().MinimumLength(4);
            RuleFor(command => command.Model.Surname).NotNull().MinimumLength(4);
        }
    }
}
