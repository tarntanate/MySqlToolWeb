using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.DeleteAdAsset
{
    public class DeleteAdAssetCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public DeleteAdAssetCommand(long id)
        {
            Id = id;
        }
    }
}
