using FluentValidation;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.IsExistsAdNetworkByPlatform
{
    public class IsExistsAdNetworkByPlatformQueryValidator : AbstractValidator<IsExistsAdNetworkByPlatformQuery>
    {
        public IsExistsAdNetworkByPlatformQueryValidator()
        {
            RuleFor(p => p.Platform)
                .Custom((value, context) =>
                {
                    if (value == Platform.Unknown)
                        context.AddFailure($"Unsupported Platform Type.");
                });
        }
    }
}
