using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public CommitUploadUrlCommand(long id)
        {
            Id = id;
        }
    }
}
