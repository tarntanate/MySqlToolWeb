using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Common.Extensions;
using System.Linq;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByAdUnitId
{
    public class GetBannerByAdUnitIdQueryValidator : AbstractValidator<GetBannerByAdUnitIdQuery>
    {
        private IMediator Mediator { get; }

        public GetBannerByAdUnitIdQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AppCode)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.AppVersion)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Platform)
                .NotNull()
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var platforms = new string[] { "Android", "iOS", "Web" };
                    if (!platforms.Contains(value))
                        context.AddFailure($"Platforms only support 'Android', 'iOS' and 'Web'");
                });

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(value), cancellationToken);
                    if (!isExistsAdUnitResult.Ok)
                        context.AddFailure(isExistsAdUnitResult.Message);
                });
        }
    }
}
