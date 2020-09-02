using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.DeleteAd
{
    public class DeleteAdCommandValidator : AbstractValidator<DeleteAdCommand>
    {
        public DeleteAdCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
