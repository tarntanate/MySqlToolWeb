using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public CommitUploadUrlCommand(long id)
        {
            Id = id;
        }
    }
}
