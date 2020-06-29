using FluentValidation;

namespace Ookbee.Ads.Application.Business.UserRole.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryValidator : AbstractValidator<GetUserRoleByIdQuery>
    {
        public GetUserRoleByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
