using Anna.Common.MongoDB;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.Advertising.Mongo
{
    public sealed class OokbeeAdsMongoDBContext : BaseContext
    {
        public OokbeeAdsMongoDBContext() : base(GlobalVar.AppSettings.ConnectionStrings.MongoDB)
        {

        }
    }
}
