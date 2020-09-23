using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQueryValidator : AbstractValidator<IsExistsPublisherByIdQuery>
    {
        public IsExistsPublisherByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
