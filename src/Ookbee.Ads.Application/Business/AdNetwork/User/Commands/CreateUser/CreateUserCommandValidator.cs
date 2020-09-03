using FluentValidation;
using MediatR;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdNetwork.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private IMediator Mediator { get; }

        public CreateUserCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0);

            RuleFor(p => p.DisplayName)
                .MaximumLength(500);

            RuleFor(p => p.AvatarUrl)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidUri())
                .WithMessage("'{PropertyName}' address is not valid");
        }
    }
}
