using FluentValidation;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Queries.GetUserRoleMappingList
{
    public class GetUserRoleMappingListQueryValidator : AbstractValidator<GetUserRoleMappingListQuery>
    {
        public GetUserRoleMappingListQueryValidator()
        {
            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
