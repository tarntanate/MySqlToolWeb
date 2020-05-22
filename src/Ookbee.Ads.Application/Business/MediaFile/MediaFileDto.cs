using System;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Application.Business.MediaFile
{
    public class MediaFileDto
    {
        public string Id { get; set; }

        public string AdId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MimeType { get; set; }

        string _mediaUrl;
        public string MediaUrl
        {
            get
            {
                var uriString = GlobalVar.AppSettings.Tencent.Cos.HostName.CDN;
                if (!uriString.HasValue())
                    uriString = GlobalVar.AppSettings.Tencent.Cos.HostName.Default;

                var uri = new Uri(new Uri(uriString), this._mediaUrl);
                return uri.AbsoluteUri;
            }
            set
            {
                this._mediaUrl = value;
            }
        }

        public string Position { get; set; }

        public string AppLink { get; set; }

        public string WebLink { get; set; }
    }
}
