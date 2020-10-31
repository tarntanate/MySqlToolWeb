using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.RequestLogs.RequestLog.Commands.CreateGroupRequestLog;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Cache.AdUnitRedis.Commands.GetAdUnitByGroupIdRedis;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListByPublisherIdRedis;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Helpers;
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
        private static readonly HttpClient HttpClient;

        static GroupsController()
        {
            HttpClient = new HttpClient();
        }

        [HttpGet("{groupId}/units")]
        public async Task<ContentResult> GetAdUnitByGroupId([FromQuery] AdPlatform platform, [FromRoute] long groupId, [FromQuery] string ookbeeId_query, [FromHeader(Name="Ookbee-Account-Id")] string ookbeeId_header, CancellationToken cancellationToken)
        {
            var kafkaKeyValue = new AdGroupRequestLogRecordRequest
            {
                Key = new AdGroupRequestLogKeyRequest
                {
                    UUID = ookbeeId_query ?? ookbeeId_header ?? "99"
                },
                Value = new AdGroupRequestLogValueRequest
                {
                    CreatedAt = MechineDateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    AdGroupId = (int)groupId,
                    PlatformId = (int) platform,
                    UUID = ookbeeId_query ?? ookbeeId_header ?? new Random().Next(0, 20).ToString(),
                }
            };

            var kafkaSchema = new GroupRequestLogSchema();
            var kafkaRequest = new AdGroupRequestLogRequest
            {
                Records = new List<AdGroupRequestLogRecordRequest>() {
                    kafkaKeyValue
                },
                ValueSchemaId = kafkaSchema.value_schema_id,
                KeySchemaId = kafkaSchema.key_schema_id
            };
            

            var adRequestLogService = new AdsRequestLogService(HttpClient);
            var kafkaResponse = await adRequestLogService.Create(kafkaRequest, cancellationToken);
            var s = kafkaResponse.StatusCode;

            // For Testing TimeScaleDb
            var timescaleResult = await Mediator.Send(
                new CreateGroupRequestLogCommand(
                    platformId: (short)platform,
                    adGroupId: (short)groupId,
                    uuid: ookbeeId_query ?? ookbeeId_header ?? new Random().Next(0, 20).ToString()),
                    cancellationToken);

            string platformString = Enum.GetName(typeof(AdPlatform), platform);
            var result = await Mediator.Send(new GetAdUnitByGroupIdRedisQuery(platformString, groupId), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }

        [HttpGet("ids")]
        public async Task<ContentResult> GetAdGroupIdListByPubliser([FromQuery] string publisher, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAdGroupIdListByPublisherIdRedisQuery(publisher), cancellationToken);
            if (result.IsSuccess)
                return Content(result.Data, "application/json");
            return new ContentResult() { StatusCode = 404 };
        }
    }
}
