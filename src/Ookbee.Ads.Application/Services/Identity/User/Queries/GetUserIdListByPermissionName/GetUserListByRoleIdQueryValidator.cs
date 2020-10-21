using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserIdListByPermissionName
{
    public class GetUserIdListByPermissionNameQueryValidator : AbstractValidator<GetUserIdListByPermissionNameQuery>
    {
        public GetUserIdListByPermissionNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
