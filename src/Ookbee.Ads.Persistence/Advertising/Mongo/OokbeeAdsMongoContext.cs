using Anna.Common.MongoDB;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.Advertising.Mongo
{
    public sealed class OokbeeAdsMongoContext : BaseContext
    {
        public OokbeeAdsMongoContext() : base(GlobalVar.AppSettings.ConnectionStrings.MongoDB)
        {

        }
    }
}
