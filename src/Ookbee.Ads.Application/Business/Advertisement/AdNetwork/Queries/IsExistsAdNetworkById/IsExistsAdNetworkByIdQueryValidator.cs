using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.IsExistsAdNetworkById
{
    public class IsExistsAdNetworkByIdQueryValidator : AbstractValidator<IsExistsAdNetworkByIdQuery>
    {
        public IsExistsAdNetworkByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
