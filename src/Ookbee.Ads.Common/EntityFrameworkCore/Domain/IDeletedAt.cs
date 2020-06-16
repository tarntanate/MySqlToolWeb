using System;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Domain
{
    public interface IDeletedAt
    {
        DateTime? DeletedAt { get; set; }
    }
}