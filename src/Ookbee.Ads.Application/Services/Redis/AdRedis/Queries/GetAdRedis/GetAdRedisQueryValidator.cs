using FluentValidation;
using Ookbee.Ads.Common.Extensions;
using System;

namespace Ookbee.Ads.Application.Services.Cache.AdRedis.Commands.GetAdRedis
{
    public class GetAdRedisQueryValidator : AbstractValidator<GetAdRedisQuery>
    {
        public GetAdRedisQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.UserIdText)
                .Custom((value, context) =>
               {
                   if (value.HasValue() && !Int64.TryParse(value, out long number))
                   {
                       context.AddFailure($"RequestHeader 'Ookbee-Account-Id' is invalid [{value}]");
                   }
               });
        }
    }
}
