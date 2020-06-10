﻿using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeByName;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.EFCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommandHandler : IRequestHandler<UpdateAdUnitTypeCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdUnitTypeEntity> AdUnitTypeEFCoreRepo { get; }

        public UpdateAdUnitTypeCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdUnitTypeEntity> adUnitTypeEFCoreRepo)
        {
            Mediator = mediator;
            AdUnitTypeEFCoreRepo = adUnitTypeEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateAdUnitTypeCommand request)
        {
            var result = new HttpResult<bool>();
            
            var adUnitTypeResult = await Mediator.Send(new GetAdUnitTypeByIdQuery(request.Id));
            if (!adUnitTypeResult.Ok)
                return result.Fail(adUnitTypeResult.StatusCode, adUnitTypeResult.Message);

            var adUnitTypeByNameResult = await Mediator.Send(new GetAdUnitTypeByNameQuery(request.Name));
            if (adUnitTypeByNameResult.Ok &&
                adUnitTypeByNameResult.Data.Id != request.Id &&
                adUnitTypeByNameResult.Data.Name == request.Name)
                return result.Fail(409, $"AdUnitType '{request.Name}' already exists.");

            var source = Mapper
                .Map(request)
                .Over(adUnitTypeResult.Data);
            var entity = Mapper
                .Map(request)
                .ToANew<AdUnitTypeEntity>();

            await AdUnitTypeEFCoreRepo.UpdateAsync(entity.Id, entity);
            await AdUnitTypeEFCoreRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
