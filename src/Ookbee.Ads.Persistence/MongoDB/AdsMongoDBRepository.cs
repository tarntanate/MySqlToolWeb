
using Anna.Common.MongoDB;
using Anna.Common.MongoDB.Domain;

namespace Ookbee.Ads.Persistence.Advertising.Mongo
{
    public sealed class AdsMongoDBRepository<TDocument> : BaseRepository<TDocument> where TDocument : BaseDocument
    {
        public AdsMongoDBRepository(AdsMongoDBContext context) : base(context)
        {

        }
    }
}
