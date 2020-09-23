using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public UpdatePublisherCommandHandler(
            IMapper mapper,
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            Mapper = mapper;
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<PublisherEntity>(request);
            await PublisherDbRepo.UpdateAsync(entity.Id, entity);
            await PublisherDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
