﻿using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupStats.Commands.ArchiveAdGroupStats
{
    public class ArchiveAdGroupStatsCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }

        public ArchiveAdGroupStatsCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}
