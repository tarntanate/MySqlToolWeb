using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.IsExistsAdGroupItemById
{
    public class IsExistsAdGroupItemByIdQueryValidator : AbstractValidator<IsExistsAdGroupItemByIdQuery>
    {
        public IsExistsAdGroupItemByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
