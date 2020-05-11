using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsByNameAdvertiser
{
    public class IsExistsByNameAdvertiserCommandValidator : AbstractValidator<IsExistsByNameAdvertiserCommand>
    {
        public IsExistsByNameAdvertiserCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
