﻿using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommand : UpdateAdStatusRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdateAdStatusCommand(long id, UpdateAdStatusRequest request)
        {
            Id = id;
            Status = request.Status;
        }
    }
}
