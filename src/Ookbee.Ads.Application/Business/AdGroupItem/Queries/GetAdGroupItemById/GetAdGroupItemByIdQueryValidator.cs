using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemById
{
    public class GetAdGroupItemByIdQueryValidator : AbstractValidator<GetAdGroupItemByIdQuery>
    {
        public GetAdGroupItemByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
