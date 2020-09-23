using FluentValidation;

namespace Ookbee.Ads.Application.Business.Identity.UserRole.Queries.GetUserRoleByName
{
    public class GetUserRoleByNameQueryValidator : AbstractValidator<GetUserRoleByNameQuery>
    {
        public GetUserRoleByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
