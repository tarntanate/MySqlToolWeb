using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.IsExistsAdGroupById
{
    public class IsExistsAdGroupByIdQueryValidator : AbstractValidator<IsExistsAdGroupByIdQuery>
    {
        public IsExistsAdGroupByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
