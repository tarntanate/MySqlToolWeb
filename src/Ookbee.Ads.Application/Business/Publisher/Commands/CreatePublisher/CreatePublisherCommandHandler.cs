﻿using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<PublisherEntity> PublisherEFCoreRepo { get; }

        public CreatePublisherCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<PublisherEntity> publisherEFCoreRepo)
        {
            Mediator = mediator;
            PublisherEFCoreRepo = publisherEFCoreRepo;
        }

        public async Task<HttpResult<long>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreatePublisherCommand request)
        {
            var result = new HttpResult<long>();

            var isExistsPublisherByName = await Mediator.Send(new IsExistsPublisherByNameQuery(request.Name));
            if (isExistsPublisherByName.Ok)
                return result.Fail(409, $"Publisher '{request.Name}' already exists.");

            var entity = Mapper
                .Map(request)
                .ToANew<PublisherEntity>();

            await PublisherEFCoreRepo.InsertAsync(entity);
            await PublisherEFCoreRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
