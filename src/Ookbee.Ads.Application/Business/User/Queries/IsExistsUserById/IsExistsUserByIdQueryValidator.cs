using FluentValidation;

namespace Ookbee.Ads.Application.Business.User.Queries.IsExistsUserById
{
    public class IsExistsUserByIdQueryValidator : AbstractValidator<IsExistsUserByIdQuery>
    {
        public IsExistsUserByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
