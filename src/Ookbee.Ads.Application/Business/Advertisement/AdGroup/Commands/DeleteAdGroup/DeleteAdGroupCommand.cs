using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.DeleteAdGroup
{
    public class DeleteAdGroupCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteAdGroupCommand(long id)
        {
            Id = id;
        }
    }
}
