using FluentValidation;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.UpdateAdUnitStatsRedis
{
    public class UpdateAdUnitStatsRedisCommandValidator : AbstractValidator<UpdateAdUnitStatsRedisCommand>
    {
        public UpdateAdUnitStatsRedisCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.StatsType)
                .Custom((value, context) =>
                {
                    if (value != AdStatsType.Request &&
                        value != AdStatsType.Fill)
                    {
                        context.AddFailure($"Only 'Request' and 'Fill' status type are allowed.");
                    }
                });
        }
    }
}
