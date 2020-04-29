using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertising.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommand : IRequest<HttpResult<string>>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public CreateAdvertiserCommand()
        {
            Id = ObjectId.GenerateNewId();
        }
    }
}
