using Ookbee.Ads.Common.Result;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ookbee.Ads.Common.Helpers
{
    public class HttpClientHelper
    {
        public static StringContent PrepareContent(object content)
        {
            if (content == null)
                return null;
            var json = JsonHelper.Serialize(content);
            var result = new StringContent(json, Encoding.UTF8, "application/json");
            return result;
        }

        public static async Task<HttpResult<TResult>> ConvertToItemResult<TResult>(HttpResponseMessage responseMessage)
        {
            var httpResult = new HttpResult<TResult>();
            var json = await responseMessage.Content.ReadAsStringAsync();
            var responseBody = JsonHelper.Deserialize<ApiItemResult<TResult>>(json);
            if (responseMessage.IsSuccessStatusCode)
                return httpResult.Success(responseBody.Data);
            return httpResult.Fail(responseMessage.StatusCode, responseBody?.Error?.Message);
        }

        public static async Task<HttpResult<ICollection<TResult>>> ConvertToListResult<TResult>(HttpResponseMessage responseMessage)
        {
            var httpResult = new HttpResult<ICollection<TResult>>();
            var json = await responseMessage.Content.ReadAsStringAsync();
            var responseBody = JsonHelper.Deserialize<ApiListResult<TResult>>(json);
            if (responseMessage.IsSuccessStatusCode)
                return httpResult.Success(responseBody.Data.Items);
            return httpResult.Fail(responseMessage.StatusCode, responseBody?.Error?.Message);
        }
    }
}
