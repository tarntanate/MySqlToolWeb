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
using System.Diagnostics;
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
            [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId,
            CancellationToken cancellationToken)
        {
            if (platform == AdPlatform.Unknown)
            {
                return new ContentResult() { StatusCode = 500, Content = "Require platform parameter!" };
            }

            AdsRequestLog kafkaRequest = CreateAdRequestLog(adId, platform, campaignId, publisherId, unitId, ookbeeId, deviceId);

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

        private static AdsRequestLog CreateAdRequestLog(long adId, AdPlatform platform, int campaignId, int publisherId, int unitId, string ookbeeId, string deviceId)
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
                    AdId = (int)adId,
                    AdUnitId = (int)unitId,
                    UnitId = (int)unitId,
                    CampaignId = (int)campaignId,
                    PublisherId = (int)publisherId,
                    PlatformId = (int)platform,
                    UUID = uuid
                }
            };

            var kafkaRequest = new AdsRequestLog
            {
                Records = new List<AdsRequestLogRecord>() {
                    kafkaKeyValue
                }
            };
            return kafkaRequest;
        }
    }
}
