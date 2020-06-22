using FluentValidation;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQueryValidator : AbstractValidator<IsExistsPublisherByIdQuery>
    {
        public IsExistsPublisherByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
