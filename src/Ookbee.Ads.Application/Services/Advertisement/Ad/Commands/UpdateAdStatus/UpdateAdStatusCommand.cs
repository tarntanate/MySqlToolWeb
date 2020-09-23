﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public AdStatus Status { get; private set; }

        public UpdateAdStatusCommand(long id, AdStatus status)
        {
            Id = id;
            Status = status;
        }

        public UpdateAdStatusCommand(long id, UpdateAdStatusRequest request)
        {
            Id = id;
            Status = request.Status;
        }
    }
}
