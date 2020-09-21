using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQueryValidator : AbstractValidator<IsExistsPublisherByIdQuery>
    {
        public IsExistsPublisherByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
