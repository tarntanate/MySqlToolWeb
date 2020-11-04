using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.IsExistsAdGroupTypeById
{
    public class IsExistsAdGroupTypeByIdQueryValidator : AbstractValidator<IsExistsAdGroupTypeByIdQuery>
    {
        public IsExistsAdGroupTypeByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}
