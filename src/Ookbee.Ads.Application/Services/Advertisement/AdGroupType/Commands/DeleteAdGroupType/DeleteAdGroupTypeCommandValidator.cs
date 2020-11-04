using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.DeleteAdGroupType
{
    public class DeleteAdGroupTypeCommandValidator : AbstractValidator<DeleteAdGroupTypeCommand>
    {
        public DeleteAdGroupTypeCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
