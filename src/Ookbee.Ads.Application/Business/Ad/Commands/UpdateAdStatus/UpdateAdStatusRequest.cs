using System.Collections.Generic;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusRequest
    {
        public AdStatus Status { get; set; }
    }
}
