using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; }

        public CommitUploadUrlCommand(string id)
        {
            Id = id;
        }
    }
}
