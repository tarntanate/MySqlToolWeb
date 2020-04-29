using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommand : IRequest<HttpResult<bool>>
    {
        public ObjectId Id { get; set; }

        public DeleteAdvertiserCommand(string id)
        {
            Id = new ObjectId(id);
        }
    }
}
