using Anna.Common.MongoDB;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Persistence.Advertising.Mongo
{
    public sealed class AdsMongoDBContext : BaseContext
    {
        public AdsMongoDBContext() : base(GlobalVar.AppSettings.ConnectionStrings.MongoDB)
        {

        }
    }
}
