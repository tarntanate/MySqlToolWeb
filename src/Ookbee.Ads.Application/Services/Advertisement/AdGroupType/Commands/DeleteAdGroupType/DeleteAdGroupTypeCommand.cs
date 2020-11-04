using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.DeleteAdGroupType
{
    public class DeleteAdGroupTypeCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public DeleteAdGroupTypeCommand(long id)
        {
            Id = id;
        }
    }
}
