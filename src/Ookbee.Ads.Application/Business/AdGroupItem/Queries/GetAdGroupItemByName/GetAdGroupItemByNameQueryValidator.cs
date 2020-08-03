using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemByName
{
    public class GetAdGroupItemByNameQueryValidator : AbstractValidator<GetAdGroupItemByNameQuery>
    {
        public GetAdGroupItemByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
