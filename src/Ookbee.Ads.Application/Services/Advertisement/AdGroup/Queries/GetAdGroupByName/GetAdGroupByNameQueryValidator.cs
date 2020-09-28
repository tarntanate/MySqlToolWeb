using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupByName
{
    public class GetAdGroupByNameQueryValidator : AbstractValidator<GetAdGroupByNameQuery>
    {
        public GetAdGroupByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
