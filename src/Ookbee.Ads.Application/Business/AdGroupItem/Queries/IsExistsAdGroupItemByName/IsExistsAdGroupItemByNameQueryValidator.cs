using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.IsExistsAdGroupItemByName
{
    public class IsExistsAdGroupItemByNameQueryValidator : AbstractValidator<IsExistsAdGroupItemByNameQuery>
    {
        public IsExistsAdGroupItemByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
            
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0);
        }
    }
}
