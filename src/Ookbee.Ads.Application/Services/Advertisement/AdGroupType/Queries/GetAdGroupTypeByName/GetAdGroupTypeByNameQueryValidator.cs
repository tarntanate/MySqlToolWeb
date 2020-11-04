using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeByName
{
    public class GetAdGroupTypeByNameQueryValidator : AbstractValidator<GetAdGroupTypeByNameQuery>
    {
        public GetAdGroupTypeByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
