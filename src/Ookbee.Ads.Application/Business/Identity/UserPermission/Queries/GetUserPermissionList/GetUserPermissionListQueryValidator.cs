using FluentValidation;

namespace Ookbee.Ads.Application.Business.Identity.UserPermission.Queries.GetUserPermissionList
{
    public class GetUserPermissionListQueryValidator : AbstractValidator<GetUserPermissionListQuery>
    {
        public GetUserPermissionListQueryValidator()
        {
            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
