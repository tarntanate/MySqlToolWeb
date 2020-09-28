using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupById
{
    public class GetAdGroupByIdQueryValidator : AbstractValidator<GetAdGroupByIdQuery>
    {
        public GetAdGroupByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
