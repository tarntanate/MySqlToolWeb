using System.Collections.Generic;

namespace Ookbee.Ads.Common.Result
{
    public class ApiItemsResult<T>
    {
        public ICollection<T> Items { get; set; }
    }
}
