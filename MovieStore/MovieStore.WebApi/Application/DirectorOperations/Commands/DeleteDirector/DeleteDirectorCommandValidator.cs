using FluentValidation;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidator:AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(command=>command.DirectorId).NotEmpty().GreaterThan(0);
        }
    }
}
