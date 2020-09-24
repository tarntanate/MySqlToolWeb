using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserListByRoleId
{
    public class GetUserListByRoleIdQueryValidator : AbstractValidator<GetUserListByRoleIdQuery>
    {
        public GetUserListByRoleIdQueryValidator()
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
