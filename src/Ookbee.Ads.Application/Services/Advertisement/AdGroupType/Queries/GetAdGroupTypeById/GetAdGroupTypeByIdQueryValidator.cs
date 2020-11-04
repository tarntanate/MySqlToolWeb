using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeById
{
    public class GetAdGroupTypeByIdQueryValidator : AbstractValidator<GetAdGroupTypeByIdQuery>
    {
        public GetAdGroupTypeByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
