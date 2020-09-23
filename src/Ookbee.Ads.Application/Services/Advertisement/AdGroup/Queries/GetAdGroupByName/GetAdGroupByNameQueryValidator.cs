using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupByName
{
    public class GetAdGroupByNameQueryValidator : AbstractValidator<GetAdGroupByNameQuery>
    {
        public GetAdGroupByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
