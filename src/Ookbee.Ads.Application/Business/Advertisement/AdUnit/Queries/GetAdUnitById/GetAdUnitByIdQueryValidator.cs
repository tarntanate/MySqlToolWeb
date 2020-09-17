using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQueryValidator : AbstractValidator<GetAdUnitByIdQuery>
    {
        public GetAdUnitByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
