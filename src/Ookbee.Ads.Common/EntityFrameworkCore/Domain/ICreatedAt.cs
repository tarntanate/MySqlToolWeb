using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Domain
{
    public interface ICreatedAt
    {
        DateTime? CreatedAt { get; set; }
    }
}