using Newtonsoft.Json;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset
{
    public class AdAssetDto : DefaultDto
    {
        public long AdId { get; set; }
        public string AssetUrl
        {
            get
            {
                Uri uri = null;
                if (AssetPath.HasValue())
                {
                    if (GlobalVar.AppSettings.Tencent.Cos.BaseUri.Default.HasValue())
                        uri = new Uri(new Uri(GlobalVar.AppSettings.Tencent.Cos.BaseUri.Default), AssetPath);
                    else
                        uri = new Uri(AssetPath);
                }
                return uri?.IsAbsoluteUri == true ? uri?.AbsoluteUri : uri?.ToString();
            }
        }
        [JsonIgnore]
        public string AssetPath { get; set; }
        public string AssetType { get; set; }
        public AdPosition Position { get; set; }
    }
}