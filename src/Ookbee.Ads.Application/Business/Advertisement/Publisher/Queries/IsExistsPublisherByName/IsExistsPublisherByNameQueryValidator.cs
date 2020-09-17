using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQueryValidator : AbstractValidator<IsExistsPublisherByNameQuery>
    {
        public IsExistsPublisherByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
