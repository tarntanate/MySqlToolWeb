using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommandValidator : AbstractValidator<DeleteAdvertiserCommand>
    {
        public DeleteAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
