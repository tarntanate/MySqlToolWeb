using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.ActivityLog.Commands.CreateActivityLog
{
    public class CreateActivityLogCommandHandler : IRequestHandler<CreateActivityLogCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<ActivityLogEntity> ActivityLogDbRepo { get; }

        public CreateActivityLogCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<ActivityLogEntity> publisherDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            ActivityLogDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateActivityLogCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<ActivityLogEntity>(request);
            await ActivityLogDbRepo.InsertAsync(entity);
            await ActivityLogDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<long>();
            return result.Success(entity.Id);
        }
    }
}
