using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.IsExistsCampaignById;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdIdList
{
    public class GetAdIdListQueryValidator : AbstractValidator<GetAdIdListQuery>
    {
        private readonly IMediator Mediator;

        public GetAdIdListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(value.Value), cancellationToken);
                        if (!isExistsAdUnitResult.IsSuccess)
                            context.AddFailure(isExistsAdUnitResult.Message);
                    }
                });

            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(value.Value), cancellationToken);
                        if (!isExistsCampaignResult.IsSuccess)
                            context.AddFailure(isExistsCampaignResult.Message);
                    }
                });
        }
    }
}
