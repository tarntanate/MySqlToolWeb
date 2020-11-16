using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.DeleteAdGroup
{
    public class DeleteAdGroupCommandValidator : AbstractValidator<DeleteAdGroupCommand>
    {
        public DeleteAdGroupCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
