using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Extensions;
using System.Linq;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAd
{
    public class UpdateAdCommandValidator : AbstractValidator<UpdateAdCommand>
    {
        private readonly IMediator Mediator;

        public UpdateAdCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Quota)
                .GreaterThan(0);

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(value), cancellationToken);
                    if (!isExistsAdResult.IsSuccess)
                        context.AddFailure(isExistsAdResult.Message);
                });

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(value), cancellationToken);
                    if (!isExistsAdUnitResult.IsSuccess)
                        context.AddFailure(isExistsAdUnitResult.Message);
                });

            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(value), cancellationToken);
                    if (!isExistsCampaignResult.IsSuccess)
                        context.AddFailure(isExistsCampaignResult.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.Status)
                .NotNull();

            RuleFor(p => p.BackgroundColor)
                .Must(value => !value.HasValue() || value.IsValidRGBHexColor())
                .WithMessage("'{PropertyName}' is not valid RGB color");

            RuleFor(p => p.ForegroundColor)
                .Must(value => !value.HasValue() || value.IsValidRGBHexColor())
                .WithMessage("'{PropertyName}' is not valid RGB color");

            RuleForEach(p => p.Analytics)
                .Must(value => value.HasValue() && value.IsValidHttp())
                .WithMessage("'{PropertyName}' is not valid HTTP(S) address");

            RuleFor(p => p.Platforms)
                .NotNull()
                .Must(value => value.Count() <= 3)
                .WithMessage("'{PropertyName}' must be 3 items or fewer");

            RuleFor(p => p.AppLink)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidUri())
                .WithMessage("'{PropertyName}' address is not valid");

            RuleFor(p => p.LinkUrl)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidHttp())
                .WithMessage("'{PropertyName}' address is not valid");
        }
    }
}
