using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public CommitUploadUrlCommand(long id)
        {
            Id = id;
        }
    }
}
