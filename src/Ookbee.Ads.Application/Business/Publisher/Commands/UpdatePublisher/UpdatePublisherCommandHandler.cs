using System.Diagnostics;
using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.ActivityLog.Commands.CreateActivityLog;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public UpdatePublisherCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request, cancellationToken);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<PublisherEntity>(request);
            await PublisherDbRepo.UpdateAsync(entity.Id, entity);
            await PublisherDbRepo.SaveChangesAsync();

            return result.Success(true, entity.Id, entity);
        }
    }
}
