using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName
{
    public class IsExistsAdvertiserByNameQueryValidator : AbstractValidator<IsExistsAdvertiserByNameQuery>
    {
        public IsExistsAdvertiserByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
