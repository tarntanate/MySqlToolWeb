using System;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerAssetDto : DefaultDto
    {
        private string assetUrl;
        public string AssetUrl
        {
            get
            {
                if (assetUrl.HasValue())
                {
                    Uri baseUri;
                    if (GlobalVar.AppSettings.Tencent.Cos.BaseUri.CDN.HasValue())
                        baseUri = new Uri(GlobalVar.AppSettings.Tencent.Cos.BaseUri.CDN);
                    else
                        baseUri = new Uri(GlobalVar.AppSettings.Tencent.Cos.BaseUri.Default);
                    return new Uri(baseUri, assetUrl).ToString();
                }
                return assetUrl;
            }
            set { assetUrl = value; }
        }
        public string AssetType { get; set; }
        public string Position { get; set; }
    }
}
