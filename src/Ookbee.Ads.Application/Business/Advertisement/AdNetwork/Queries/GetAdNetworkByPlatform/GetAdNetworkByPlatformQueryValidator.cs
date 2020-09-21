using FluentValidation;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform
{
    public class GetAdNetworkByPlatformQueryValidator : AbstractValidator<GetAdNetworkByPlatformQuery>
    {
        public GetAdNetworkByPlatformQueryValidator()
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
