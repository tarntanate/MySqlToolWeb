using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupById
{
    public class GetAdGroupByIdQueryValidator : AbstractValidator<GetAdGroupByIdQuery>
    {
        public GetAdGroupByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
