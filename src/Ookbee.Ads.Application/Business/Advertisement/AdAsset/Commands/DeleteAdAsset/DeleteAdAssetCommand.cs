using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.DeleteAdAsset
{
    public class DeleteAdAssetCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeleteAdAssetCommand(long id)
        {
            Id = id;
        }
    }
}
