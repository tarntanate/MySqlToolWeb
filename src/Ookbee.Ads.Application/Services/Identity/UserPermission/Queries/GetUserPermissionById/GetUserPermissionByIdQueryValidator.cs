using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Queries.GetUserPermissionById
{
    public class GetUserPermissionByIdQueryValidator : AbstractValidator<GetUserPermissionByIdQuery>
    {
        public GetUserPermissionByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}
