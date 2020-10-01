using FluentValidation;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupRedis.Commands.UpdateAdGroupStatsRedis
{
    public class UpdateAdGroupStatsRedisCommandValidator : AbstractValidator<UpdateAdGroupStatsRedisCommand>
    {
        public UpdateAdGroupStatsRedisCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.StatsType)
                .Custom((value, context) =>
                {
                    if (value != AdStatsType.Request)
                    {
                        context.AddFailure($"Only 'Request' status type are allowed.");
                    }
                });
        }
    }
}
