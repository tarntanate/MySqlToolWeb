using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Domain
{
    public interface IUpdatedAt
    {
        DateTime? UpdatedAt { get; set; }
    }
}