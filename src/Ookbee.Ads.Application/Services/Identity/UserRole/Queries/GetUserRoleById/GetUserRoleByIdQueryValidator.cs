using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryValidator : AbstractValidator<GetUserRoleByIdQuery>
    {
        public GetUserRoleByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}
