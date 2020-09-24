using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ookbee.Ads.Common.Helpers
{
    public static class HttpClientHelper
    {
        public static StringContent PrepareContent(object content)
        {
            if (content == null)
                return null;
            var json = JsonHelper.Serialize(content);
            var result = new StringContent(json, Encoding.UTF8, "application/json");
            return result;
        }

        public static async Task<Response<TResult>> ConvertToItemResult<TResult>(HttpResponseMessage responseMessage)
        {
            var httpResult = new Response<TResult>();
            var json = await responseMessage.Content.ReadAsStringAsync();
            var responseBody = JsonHelper.Deserialize<ApiItemResult<TResult>>(json);
            if (responseMessage.IsSuccessStatusCode)
                return httpResult.OK(responseBody.Data);
            return httpResult.Status(responseMessage.StatusCode, responseBody?.Error?.Message);
        }

        public static async Task<Response<ICollection<TResult>>> ConvertToListResult<TResult>(HttpResponseMessage responseMessage)
        {
            var httpResult = new Response<ICollection<TResult>>();
            var json = await responseMessage.Content.ReadAsStringAsync();
            var responseBody = JsonHelper.Deserialize<ApiListResult<TResult>>(json);
            if (responseMessage.IsSuccessStatusCode)
                return httpResult.OK(responseBody.Data.Items);
            return httpResult.Status(responseMessage.StatusCode, responseBody?.Error?.Message);
        }
    }
}
