using FluentValidation;

namespace Ookbee.Ads.Application.Business.UserPermission.Queries.GetUserPermissionById
{
    public class GetUserPermissionByIdQueryValidator : AbstractValidator<GetUserPermissionByIdQuery>
    {
        public GetUserPermissionByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
