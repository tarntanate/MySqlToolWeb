using Microsoft.AspNetCore.Mvc;
using Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog;
using Ookbee.Ads.Application.Business.RequestLogs.AdImpressionLog.Commands.CreateAdImpressionLog;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.UpdateAdUnitStatsRedis;
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
    [Route("api/[controller]/{adUnitId}/stats")]
    public class UnitsController : ApiController
    {
        private static readonly HttpClient HttpClient;

        static UnitsController()
        {
            HttpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<ContentResult> UpdateUnitStats(
            [FromRoute] long adUnitId,
            [FromQuery] AdPlatform platform,
            [FromQuery] int campaignId,
            [FromQuery] int publisherId,
            [FromQuery] string type,
            [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId,
            CancellationToken cancellationToken)
        {
            if (platform == AdPlatform.Unknown)
            {
                return new ContentResult() { StatusCode = 500, Content = "Require 'platform' parameter!" };
            }

            AdsRequestLog kafkaRequest = CreateAdRequestLog(adUnitId, platform, campaignId, publisherId, ookbeeId, deviceId);

            var adRequestLogService = new AdsRequestLogService(HttpClient);

            var _type = type.ToEnum<AdStatsType>();
            if (_type == AdStatsType.Impression)
            {
                var adImpressionLogSchema = new AdImpressionLogSchema();
                kafkaRequest.KeySchemaId = adImpressionLogSchema.KeySchemaId;
                kafkaRequest.ValueSchemaId = adImpressionLogSchema.ValueSchemaId;
                await adRequestLogService.Create("topics/adimpressionlog", kafkaRequest, cancellationToken);
            }
            if (_type == AdStatsType.Click)
            {
                var adClickLogSchema = new AdClickLogSchema();
                kafkaRequest.KeySchemaId = adClickLogSchema.KeySchemaId;
                kafkaRequest.ValueSchemaId = adClickLogSchema.ValueSchemaId;
                await adRequestLogService.Create("topics/adclicklog", kafkaRequest, cancellationToken);
            }

            var result = await Mediator.Send(new UpdateAdUnitStatsRedisCommand(adUnitId, type.ToEnum<AdStatsType>()), cancellationToken);
            if (result.IsSuccess)
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404, Content = result.Message };
        }

        private static AdsRequestLog CreateAdRequestLog(long adUnitId, AdPlatform platform, int campaignId, int publisherId, string ookbeeId, string deviceId)
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
                    AdsGroupId = 0,
                    AdId = 0,
                    AdUnitId = (int)adUnitId,
                    UnitId = (int)adUnitId,
                    CampaignId = (int)campaignId,
                    PlatformId = (int)platform,
                    PublisherId = (int)publisherId,
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
