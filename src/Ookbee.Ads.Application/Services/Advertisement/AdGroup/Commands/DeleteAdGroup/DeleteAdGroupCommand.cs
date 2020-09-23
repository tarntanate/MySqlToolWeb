using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.DeleteAdGroup
{
    public class DeleteAdGroupCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteAdGroupCommand(long id)
        {
            Id = id;
        }
    }
}
