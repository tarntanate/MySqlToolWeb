
using Anna.Common.MongoDB;
using Anna.Common.MongoDB.Domain;

namespace Ookbee.Ads.Persistence.Advertising.Mongo
{
    public sealed class OokbeeAdsMongoRepository<TDocument> : BaseRepository<TDocument> where TDocument : BaseDocument
    {
        public OokbeeAdsMongoRepository(OokbeeAdsMongoContext context) : base(context)
        {

        }
    }
}
