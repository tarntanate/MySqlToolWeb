using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.CreateAdGroupIdCache
{
    public class CreateAdGroupIdCacheCommandValidator : AbstractValidator<CreateAdGroupIdCacheCommand>
    {
        public CreateAdGroupIdCacheCommandValidator(IMediator mediator)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0);
        }
    }
}
