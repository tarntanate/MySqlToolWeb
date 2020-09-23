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

        public AdsRequestLogService(HttpClient client)
        {
            Client = client;
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/vnd.kafka.avro.v1+json");
            BaseUrl = GlobalVar.AppSettings.Services.Ads.RequestLog.BaseUri.Internal;
        }

        public async Task<Response<ApiItemResult<AdsRequestLogResponse>>> Create(AdsRequestLogRequest data, CancellationToken cancellationToken)
        {
            return await Create(new List<AdsRequestLogRequest>() { data }, cancellationToken);
        }

        public async Task<Response<ApiItemResult<AdsRequestLogResponse>>> Create(IEnumerable<AdsRequestLogRequest> data, CancellationToken cancellationToken)
        {
            var content = HttpClientHelper.PrepareContent(data);
            var httpResponse = await Client.PostAsync($"{BaseUrl}/topics/adsrequestlog", content, cancellationToken);
            var response = await HttpClientHelper.ConvertToItemResult<ApiItemResult<AdsRequestLogResponse>>(httpResponse);
            return response;
        }
    }
}
