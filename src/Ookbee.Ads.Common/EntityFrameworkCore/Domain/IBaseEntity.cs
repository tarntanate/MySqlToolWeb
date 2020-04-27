using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Domain
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset UpdatedDate { get; set; }
    }
}