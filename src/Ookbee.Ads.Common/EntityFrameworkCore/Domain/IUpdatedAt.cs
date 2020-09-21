using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Domain
{
    public interface IUpdatedAt
    {
        DateTimeOffset? UpdatedAt { get; set; }
    }
}