using Anna.Common.MongoDB;
using Anna.Common.MongoDB.Domain;

namespace Ookbee.Ads.Persistence.Advertising.Mongo.AdsMongo
{
    public sealed class AdsMongoRepository<TDocument> : BaseRepository<TDocument> where TDocument : BaseDocument
    {
        public AdsMongoRepository(AdsMongoContext context) : base(context)
        {

        }
    }
}
