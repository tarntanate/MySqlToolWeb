using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.IsExistsAdById
{
    public class IsExistsAdByIdQueryValidator : AbstractValidator<IsExistsAdByIdQuery>
    {
        public IsExistsAdByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
