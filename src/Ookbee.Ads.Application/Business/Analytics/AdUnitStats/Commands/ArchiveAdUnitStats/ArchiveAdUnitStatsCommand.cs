﻿using MediatR;
using System;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitStats.Commands.ArchiveAdUnitStats
{
    public class ArchiveAdUnitStatsCommand : IRequest<Unit>
    {
        public DateTimeOffset CaculatedAt { get; set; }

        public ArchiveAdUnitStatsCommand(DateTimeOffset caculatedAt)
        {
            CaculatedAt = caculatedAt;
        }
    }
}