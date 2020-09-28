using FluentValidation;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform
{
    public class GetAdNetworkByPlatformQueryValidator : AbstractValidator<GetAdNetworkByPlatformQuery>
    {
        public GetAdNetworkByPlatformQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Platform)
                .Custom((value, context) =>
               {
                   if (value == AdPlatform.Unknown)
                       context.AddFailure($"Unsupported Platform Type.");
               });
        }
    }
}
