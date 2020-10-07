using Ookbee.Ads.Common.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ookbee.Ads.Common.Helpers
{
    public static class HttpClientHelper
    {
        public static StringContent PrepareContent(object content, string contentType = "application/json", bool useCamelCasePropertyName = false)
        {
            if (content == null)
                return null;
            var json = JsonHelper.Serialize(content, useCamelCasePropertyName);
            var result = new StringContent(json, Encoding.UTF8, contentType);
            return result;
        }

        public static async Task<Response<TResult>> ConvertToItemResult<TResult>(HttpResponseMessage responseMessage)
        {
            var httpResult = new Response<TResult>();
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var responseObject = JsonHelper.Deserialize<TResult>(json);
                return httpResult.OK(responseObject);
            }
            return httpResult.Status(responseMessage.StatusCode, responseMessage.ReasonPhrase);
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
