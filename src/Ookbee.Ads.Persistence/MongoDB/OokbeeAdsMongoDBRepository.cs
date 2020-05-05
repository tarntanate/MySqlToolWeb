
using Anna.Common.MongoDB;
using Anna.Common.MongoDB.Domain;

namespace Ookbee.Ads.Persistence.Advertising.Mongo
{
    public sealed class OokbeeAdsMongoDBRepository<TDocument> : BaseRepository<TDocument> where TDocument : BaseDocument
    {
        public OokbeeAdsMongoDBRepository(OokbeeAdsMongoDBContext context) : base(context)
        {

        }
    }
}
