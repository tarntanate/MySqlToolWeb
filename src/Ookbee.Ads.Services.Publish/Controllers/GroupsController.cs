using Microsoft.AspNetCore.Mvc;
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
        public async Task<ContentResult> GetAdUnitByGroupId([FromQuery] AdPlatform platform, [FromRoute] long groupId, [FromQuery] string ookbeeId, CancellationToken cancellationToken)
        {
            var kafkaKeyValue = new AdsRequestLogRecordRequest
            {
                Key = new AdsRequestLogKeyRequest
                {
                    UUID = ookbeeId ?? "99"
                },
                Value = new AdsRequestLogValueRequest
                {
                    CreatedAt = MechineDateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    AdsGroupId = (int)groupId,
                    PlatformId = (int) platform,
                    UUID = ookbeeId ?? new Random().Next(0, 20).ToString(),
                    RequestTypeId = 1
                }
            };

            var kafkaRequest = new AdsRequestLogRequest
            {
                Records = new List<AdsRequestLogRecordRequest>() {
                    kafkaKeyValue
                },
                ValueSchemaId = 45,
                KeySchemaId = 41
            };
            

            var adRequestLogService = new AdsRequestLogService(HttpClient);
            var kafkaResponse = await adRequestLogService.Create(kafkaRequest, cancellationToken);
            var s = kafkaResponse.StatusCode;

            // For Testing TimeScaleDb
            // var timescaleResult = await Mediator.Send(
            //     new CreateGroupRequestLogCommand(
            //         platformId: (short)platform,
            //         adGroupId: (short)groupId,
            //         uuid: new Random().Next(0, 20).ToString()),
            //         cancellationToken);

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
