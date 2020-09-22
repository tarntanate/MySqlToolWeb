using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.DeleteAdAsset
{
    public class DeleteAdAssetCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeleteAdAssetCommand(long id)
        {
            Id = id;
        }
    }
}
