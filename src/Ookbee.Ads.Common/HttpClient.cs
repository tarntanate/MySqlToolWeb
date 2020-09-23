using Ookbee.Ads.Common.Helpers;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ookbee.Ads.Common
{
    public class BaseHttpClient
    {
        private HttpClient HttpClient { get; }

        protected BaseHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        protected async Task<T> Get<T>(string uri)
        {
            var request = CreateRequest(HttpMethod.Get, uri);
            var response = await ExecuteRequest<T>(request);
            return response;
        }

        protected async Task<T> Post<T>(string uri, object content)
        {
            var request = CreateRequest(HttpMethod.Post, uri, content);
            var response = await ExecuteRequest<T>(request);
            return response;
        }

        private static HttpRequestMessage CreateRequest(HttpMethod httpMethod, string uri, object content = null)
        {
            var request = new HttpRequestMessage(httpMethod, uri);
            if (content == null)
                return request;

            var json = JsonHelper.Serialize(content);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return request;
        }

        private async Task<T> ExecuteRequest<T>(HttpRequestMessage request)
        {
            try
            {
                var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = string.IsNullOrEmpty(responseContent) ? default : JsonHelper.Deserialize<T>(responseContent);

                return responseObject;
            }
            catch (Exception ex)
            {
                throw new Exception("HttpClient exception: ", ex);
            }
        }
    }
}
