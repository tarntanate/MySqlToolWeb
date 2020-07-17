using Newtonsoft.Json;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Enums;
using System;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerAssetDto : DefaultDto
    {
        public string AssetType { get; set; }

        public string AssetUrl
        {
            get
            {
                Uri uri = null;
                if (AssetPath.HasValue())
                {
                    if (GlobalVar.AppSettings.Tencent.Cos.BaseUri.CDN.HasValue())
                        uri = new Uri(new Uri(GlobalVar.AppSettings.Tencent.Cos.BaseUri.CDN), AssetPath);

                    else if (GlobalVar.AppSettings.Tencent.Cos.BaseUri.Default.HasValue())
                        uri = new Uri(new Uri(GlobalVar.AppSettings.Tencent.Cos.BaseUri.Default), AssetPath);

                    else
                        uri = new Uri(AssetPath);
                }
                return uri?.IsAbsoluteUri == true ? uri?.AbsoluteUri : uri?.ToString() ?? string.Empty;
            }
        }
        public Position Position { get; set; }

        [JsonIgnore]
        public string AssetPath { get; set; }
    }
}
