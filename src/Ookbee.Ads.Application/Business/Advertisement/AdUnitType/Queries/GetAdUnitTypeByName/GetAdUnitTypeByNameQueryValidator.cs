using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.GetAdUnitTypeByName
{
    public class GetAdUnitTypeByNameQueryValidator : AbstractValidator<GetAdUnitTypeByNameQuery>
    {
        public GetAdUnitTypeByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
