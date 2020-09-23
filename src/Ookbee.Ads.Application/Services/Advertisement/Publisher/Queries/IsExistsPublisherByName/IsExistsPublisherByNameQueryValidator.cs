using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQueryValidator : AbstractValidator<IsExistsPublisherByNameQuery>
    {
        public IsExistsPublisherByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
