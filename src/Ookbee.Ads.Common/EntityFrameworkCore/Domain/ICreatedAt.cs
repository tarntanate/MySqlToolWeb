using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Domain
{
    public interface ICreatedAt
    {
        DateTimeOffset? CreatedAt { get; set; }
    }
}