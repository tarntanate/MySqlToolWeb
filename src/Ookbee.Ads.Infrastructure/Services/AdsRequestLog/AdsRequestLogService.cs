using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Infrastructure.Services.AdsRequestLog
{
    public class AdsRequestLogService
    {
        private HttpClient Client { get; }
        private string BaseUrl { get; }

        private readonly string contentType = "application/vnd.kafka.avro.v1+json";

        public AdsRequestLogService(HttpClient client)
        {
            Client = client;
            BaseUrl = GlobalVar.AppSettings.Services.Ads.RequestLog.BaseUri.Internal;
        }

        private HttpRequestMessage CreateHttpRequest(HttpMethod httpMethod, string uri, object data = null, string contentType = "application/json")
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new System.Uri(uri),
                Method = httpMethod,
                Headers = {
                    { System.Net.HttpRequestHeader.ContentType.ToString(), contentType },
                }
            };
            if (data == null)
                return request;

            var content = HttpClientHelper.PrepareContent(data, contentType, false);
            request.Content = content;

            return request;
        }

        public async Task<Response<AdsRequestLogResponse>> Create(string url, Models.AdsRequestLog data, CancellationToken cancellationToken)
        {
            var request = this.CreateHttpRequest(HttpMethod.Post, $"{BaseUrl}/{url}", data, contentType); // HttpClientHelper.PrepareContent(data);
            var httpResponse = await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            // httpResponse.EnsureSuccessStatusCode();
            var response = await HttpClientHelper.ConvertToItemResult<AdsRequestLogResponse>(httpResponse);
            return response;
        }

        public async Task<Response<ApiItemResult<AdsRequestLogResponse>>> Create(string url, IEnumerable<Models.AdsRequestLog> data, CancellationToken cancellationToken)
        {
            var content = HttpClientHelper.PrepareContent(data);
            var httpResponse = await Client.PostAsync($"{BaseUrl}/{url}", content, cancellationToken);
            var response = await HttpClientHelper.ConvertToItemResult<ApiItemResult<AdsRequestLogResponse>>(httpResponse);
            return response;
        }
    }
}
