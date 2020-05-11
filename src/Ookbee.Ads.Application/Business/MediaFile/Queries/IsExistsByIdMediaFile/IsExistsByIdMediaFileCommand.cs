using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.IsExistsByIdMediaFile
{
    public class IsExistsByIdMediaFileCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public IsExistsByIdMediaFileCommand(string id)
        {
            Id = id;
        }
    }
}
