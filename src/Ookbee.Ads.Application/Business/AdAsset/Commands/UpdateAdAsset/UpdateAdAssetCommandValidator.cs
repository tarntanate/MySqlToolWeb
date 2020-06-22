using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Common.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommandValidator : AbstractValidator<UpdateAdAssetCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdAssetCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.AssetPath)
                .Must(value => value.HasValue() && value.IsValidHttp())
                .WithMessage("'{PropertyName}' is not valid HTTP(S) address");

            RuleFor(p => p.AdId)
                .CustomAsync(BeAValidAdId);
        }

        private async Task BeAValidAdId(long value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsAdByIdQuery(value), cancellationToken);
            if (!result.Ok)
                context.AddFailure(result.Message);
        }
    }
}
