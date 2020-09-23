using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.GetUserRoleList
{
    public class GetUserRoleListQueryValidator : AbstractValidator<GetUserRoleListQuery>
    {
        public GetUserRoleListQueryValidator()
        {
            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
