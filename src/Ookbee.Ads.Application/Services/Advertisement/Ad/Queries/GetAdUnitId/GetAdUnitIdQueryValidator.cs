using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdUnitId
{
    public class GetAdUnitIdQueryValidator : AbstractValidator<GetAdUnitIdQuery>
    {
        public GetAdUnitIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
