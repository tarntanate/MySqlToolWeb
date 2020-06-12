using Anna.Common.MongoDB;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.Advertising.Mongo.AdsMongo
{
    public sealed class AdsMongoContext : BaseContext
    {
        public AdsMongoContext() : base(GlobalVar.AppSettings.ConnectionStrings.MongoDB)
        {

        }
    }
}
