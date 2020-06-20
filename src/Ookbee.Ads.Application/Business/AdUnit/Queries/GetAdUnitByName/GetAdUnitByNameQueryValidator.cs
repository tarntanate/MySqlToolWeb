using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitByName
{
    public class GetAdUnitByNameQueryValidator : AbstractValidator<GetAdUnitByNameQuery>
    {
        public GetAdUnitByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
