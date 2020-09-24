﻿using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }

        public DeletePublisherCommand(long id)
        {
            Id = id;
        }
    }
}
