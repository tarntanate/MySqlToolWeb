using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Services.CacheManager.AdGroupCache.Commands.DeleteAdGroupIdCache
{
    public class DeleteAdGroupIdCacheCommandValidator : AbstractValidator<DeleteAdGroupIdCacheCommand>
    {
        public DeleteAdGroupIdCacheCommandValidator(IMediator mediator)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0);
        }
    }
}
