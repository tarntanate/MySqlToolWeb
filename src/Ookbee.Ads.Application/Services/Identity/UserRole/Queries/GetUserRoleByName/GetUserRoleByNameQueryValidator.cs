using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleByName
{
    public class GetUserRoleByNameQueryValidator : AbstractValidator<GetUserRoleByNameQuery>
    {
        public GetUserRoleByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
