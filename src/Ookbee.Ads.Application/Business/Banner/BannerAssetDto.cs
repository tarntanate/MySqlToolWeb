using System;
using Newtonsoft.Json;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerAssetDto : DefaultDto
    {
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
        [JsonIgnore]
        public string AssetPath { get; set; }
        public string AssetType { get; set; }
        public Position Position { get; set; }
    }
}
