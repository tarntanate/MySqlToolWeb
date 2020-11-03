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
            [FromQuery] string type,
            [FromHeader(Name = "Ookbee-Account-Id")] string ookbeeId_header,
            [FromHeader(Name = "Ookbee-Device-Id")] string deviceId_header,
            CancellationToken cancellationToken)
        {
            if (platform == AdPlatform.Unknown)
            {
                return new ContentResult() { StatusCode = 500, Content = "Require 'platform' parameter!" };
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
                    AdsGroupId = 0,
                    AdId = 0,
                    AdUnitId = (int)adUnitId,
                    UnitId = (int)adUnitId,
                    CampaignId = (int)campaignId,
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
                kafkaRequest.KeySchemaId = 49;
                kafkaRequest.ValueSchemaId = 48;
                var kafkaResponse = await adRequestLogService.Create("topics/adimpressionlog", kafkaRequest, cancellationToken);
                var s = kafkaResponse.StatusCode;
                // var timescaleResult = await Mediator.Send(
                //         new CreateAdImpressionLogCommand(
                //             platformId: (short)platform,
                //             adId: 0,
                //             unitId: (int)adUnitId,
                //             campaignId: 0,
                //             uuid: new Random().Next(0, 20).ToString()),
                //             cancellationToken);
            }
            if (_type == AdStatsType.Click)
            {
                kafkaRequest.KeySchemaId = 51;
                kafkaRequest.ValueSchemaId = 50;
                var kafkaResponse = await adRequestLogService.Create("topics/adclicklog", kafkaRequest, cancellationToken);
                var s = kafkaResponse.StatusCode;
                // var timescaleResult = await Mediator.Send(
                //         new CreateAdClickLogCommand(
                //             platformId: (short)platform,
                //             adId: 0,
                //             unitId: (int)adUnitId,
                //             campaignId: 0,
                //             uuid: new Random().Next(0, 20).ToString()),
                //             cancellationToken);
            }

            var result = await Mediator.Send(new UpdateAdUnitStatsRedisCommand(adUnitId, type.ToEnum<AdStatsType>()), cancellationToken);
            if (result.IsSuccess)
                return new ContentResult() { StatusCode = 200 };
            return new ContentResult() { StatusCode = 404, Content = result.Message };
        }
    }
}
