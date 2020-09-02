using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQueryValidator : AbstractValidator<IsExistsAdByNameQuery>
    {
        public IsExistsAdByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
