using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Cache.AdUnitRedis.Commands.GetAdUnitByGroupIdRedis;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherIdRedis;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Infrastructure.Services.AdsRequestLog;
using Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Publish.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ApiController
    {
        private static readonly HttpClient httpClient;
        private static readonly AdsRequestLogService adsRequestLogService;

        static GroupsController()
        {
            httpClient = new HttpClient();
            adsRequestLogService = new AdsRequestLogService(httpClient);
        }

        [HttpGet("{groupId}/units")]
        public async Task<ContentResult> GetAdUnitByGroupId(
            [FromQuery] AdPlatform platform,
            [FromRoute] long groupId,
            [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId,
            CancellationToken cancellationToken)
        {
            AdsRequestLog kafkaRequest = CreateAdGroupRequestLog(platform, groupId, ookbeeId, deviceId);

            // var adRequestLogService = new AdsRequestLogService(HttpClient);
            await adsRequestLogService.Create("topics/grouprequestlog", kafkaRequest, cancellationToken);

            string platformString = Enum.GetName(typeof(AdPlatform), platform);
            var result = await Mediator.Send(new GetAdUnitByGroupIdRedisQuery(platformString, groupId), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }

        [HttpGet("ids")]
        public async Task<ContentResult> GetAdGroupIdListByPubliser([FromHeader(Name = "Ookbee-App-Language")] string lange, [FromQuery] string publisher, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdGroupIdListByPublisherIdRedisQuery(publisher, lange), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }

        private static AdsRequestLog CreateAdGroupRequestLog(AdPlatform platform, long groupId, string ookbeeId, string deviceId)
        {
            var uuid = ookbeeId ?? deviceId ?? "0";
            if (uuid.Length > 32)
            {
                uuid = uuid.Substring(0, 32);
            }
            var kafkaKeyValue = new AdsRequestLogRecord
            {
                Key = new AdsRequestLogKey
                {
                    UUID = uuid
                },
                Value = new AdsRequestLogValue
                {
                    CreatedAt = MechineDateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    AdsGroupId = (int)groupId,
                    PlatformId = (int)platform,
                    UUID = uuid
                }
            };

            var adGroupKafkaSchema = new GroupRequestLogSchema();
            var kafkaRequest = new AdsRequestLog
            {
                Records = new List<AdsRequestLogRecord>() {
                    kafkaKeyValue
                },
                ValueSchemaId = adGroupKafkaSchema.ValueSchemaId,
                KeySchemaId = adGroupKafkaSchema.KeySchemaId
            };
            return kafkaRequest;
        }
    }
}
