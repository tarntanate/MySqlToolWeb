using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog;
using Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.UpdateAdStatsRedis;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.AspNetCore.Controllers;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Infrastructure.Services.AdsRequestLog;
using Ookbee.Ads.Infrastructure.Services.AdsRequestLog.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Services.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]/{adId}/stats")]
    public class AdsController : ApiController
    {
        private static readonly HttpClient HttpClient;

        static AdsController()
        {
            HttpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<ContentResult> UpdateAdStats(
            [FromRoute] long adId,
            [FromQuery] string type,
            [FromQuery] AdPlatform platform,
            [FromQuery] int campaignId,
            [FromQuery] int publisherId,
            [FromQuery] int unitId,
            [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId_header,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId_header,
            CancellationToken cancellationToken)
        {
            if (platform == AdPlatform.Unknown)
            {
                return new ContentResult() { StatusCode = 500, Content = "Require platform parameter!" };
            }

            var uuid = ookbeeId_header ?? deviceId_header ?? "0";
            var kafkaKeyValue = new AdGroupRequestLogRecordRequest
            {
                Key = new AdGroupRequestLogKeyRequest
                {
                    UUID = uuid
                },
                Value = new AdsRequestLogValueRequest
                {
                    CreatedAt = MechineDateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    AdId = (int)adId,
                    AdUnitId = (int)unitId,
                    UnitId = (int)unitId,
                    CampaignId = (int)campaignId,
                    PublisherId = (int)publisherId,
                    PlatformId = (int)platform,
                    UUID = uuid
                }
            };

            var kafkaRequest = new AdGroupRequestLogRequest
            {
                Records = new List<AdGroupRequestLogRecordRequest>() {
                    kafkaKeyValue
                }
            };

            var adRequestLogService = new AdsRequestLogService(HttpClient);

            var _type = type.ToEnum<AdStatsType>();
            if (_type == AdStatsType.Impression)
            {
                var adImpressionLogSchema = new AdImpressionLogSchema();
                kafkaRequest.KeySchemaId = adImpressionLogSchema.KeySchemaId;
                kafkaRequest.ValueSchemaId = adImpressionLogSchema.ValueSchemaId;
                var kafkaResponse = await adRequestLogService.Create("topics/adimpressionlog", kafkaRequest, cancellationToken);
            }
            if (_type == AdStatsType.Click)
            {
                var adClickLogSchema = new AdClickLogSchema();
                kafkaRequest.KeySchemaId = adClickLogSchema.KeySchemaId;
                kafkaRequest.ValueSchemaId = adClickLogSchema.ValueSchemaId;
                var kafkaResponse = await adRequestLogService.Create("topics/adclicklog", kafkaRequest, cancellationToken);
            }

            var result = await Mediator.Send(new UpdateAdStatsRedisCommand(adId, type.ToEnum<AdStatsType>(), 1), cancellationToken);
            if (result.IsSuccess)
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404, Content = result.Message };
        }
    }
}
