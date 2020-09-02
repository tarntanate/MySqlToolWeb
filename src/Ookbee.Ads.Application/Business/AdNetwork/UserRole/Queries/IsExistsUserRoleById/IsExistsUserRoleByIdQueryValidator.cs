using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.IsExistsUserRoleById
{
    public class IsExistsUserRoleByIdQueryValidator : AbstractValidator<IsExistsUserRoleByIdQuery>
    {
        public IsExistsUserRoleByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
