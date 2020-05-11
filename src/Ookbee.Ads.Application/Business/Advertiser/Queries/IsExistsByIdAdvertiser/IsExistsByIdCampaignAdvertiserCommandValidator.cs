using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsByIdAdvertiser
{
    public class IsExistsByIdAdvertiserCommandValidator : AbstractValidator<IsExistsByIdAdvertiserCommand>
    {
        public IsExistsByIdAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
