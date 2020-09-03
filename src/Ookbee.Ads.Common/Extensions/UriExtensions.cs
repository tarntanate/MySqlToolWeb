using System;
using System.Web;

namespace Ookbee.Ads.Common.Extensions
{
    public static class UriExtensions
    {
        public static Uri AddQueryString(this Uri uri, string name, string value)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);

            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Query = httpValueCollection.ToString();

            return uriBuilder.Uri;
        }
    }
}
