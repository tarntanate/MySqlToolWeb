﻿using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetRequest
    {
        public long AdId { get; set; }
        public string AssetPath { get; set; }
        public AssetType AssetType { get; set; }
        public Position Position { get; set; }
    }
}