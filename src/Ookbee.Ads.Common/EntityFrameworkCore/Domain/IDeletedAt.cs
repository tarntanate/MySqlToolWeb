using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Domain
{
    public interface IDeletedAt
    {
        DateTimeOffset? DeletedAt { get; set; }
    }
}