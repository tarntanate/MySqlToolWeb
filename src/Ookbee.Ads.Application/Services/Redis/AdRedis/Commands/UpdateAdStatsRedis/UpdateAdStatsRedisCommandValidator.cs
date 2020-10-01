using FluentValidation;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.UpdateAdStatsRedis
{
    public class UpdateAdStatsRedisCommandValidator : AbstractValidator<UpdateAdStatsRedisCommand>
    {
        public UpdateAdStatsRedisCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.StatsType)
                .Custom((value, context) =>
                {
                    if (value != AdStatsType.Click &&
                        value != AdStatsType.Impression)
                    {
                        context.AddFailure($"Only 'Click' and 'Impression' status type are allowed.");
                    }
                });
        }
    }
}
