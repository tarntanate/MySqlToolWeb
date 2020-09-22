using System.Collections.Generic;

namespace Ookbee.Ads.Common.Response
{
    public class ApiItemsResult<T>
    {
        public ICollection<T> Items { get; set; }
    }
}
