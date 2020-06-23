using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQueryValidator : AbstractValidator<GetAdListQuery>
    {
        private IMediator Mediator { get; }

        public GetAdListQueryValidator(IMediator mediator)
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
                .LessThanOrEqualTo(long.MaxValue)
                .CustomAsync(BeValidAdUnitId);

            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .CustomAsync(BeValidCampaignId);
        }

        private async Task BeValidAdUnitId(long? value, CustomContext context, CancellationToken cancellationToken)
        {
            if (value != null)
            {
                var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(value.Value));
                if (!isExistsAdUnitResult.Ok)
                    context.AddFailure(isExistsAdUnitResult.Message);
            }
        }

        private async Task BeValidCampaignId(long? value, CustomContext context, CancellationToken cancellationToken)
        {
            if (value != null)
            {
                var x = value;
                var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(value.Value));
                if (!isExistsCampaignResult.Ok)
                    context.AddFailure(isExistsCampaignResult.Message);
            }
        }
    }
}
