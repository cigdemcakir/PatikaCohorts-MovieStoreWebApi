using FluentValidation;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(command => command.RefreshToken).NotEmpty();
        }
    }
}
