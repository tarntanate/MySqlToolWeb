using FluentValidation;
using MediatR;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherIdRedis
{
    public class GetAdGroupIdListByPublisherIdRedisValidator : AbstractValidator<GetAdGroupIdListByPublisherIdRedisQuery>
    {
        public GetAdGroupIdListByPublisherIdRedisValidator(IMediator mediator)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.PublisherName)
                .Custom((value, context) =>
                {
                    if (!value.HasValue())
                        context.AddFailure("Request Query 'Publisher' does not exist");
                })
               .MaximumLength(40);

            RuleFor(p => p.PublisherCountry)
                .Custom((value, context) =>
                {
                    if (!value.HasValue())
                        context.AddFailure("Request Header 'Ookbee-App-Language' does not exist");
                })
                .MinimumLength(2)
                .MaximumLength(10);
        }
    }
}
