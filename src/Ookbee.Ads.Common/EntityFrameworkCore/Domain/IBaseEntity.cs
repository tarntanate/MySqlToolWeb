using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Domain
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}