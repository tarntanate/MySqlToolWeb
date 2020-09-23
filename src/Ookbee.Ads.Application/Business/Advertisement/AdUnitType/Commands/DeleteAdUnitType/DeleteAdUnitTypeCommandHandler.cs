using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommandHandler : IRequestHandler<DeleteAdUnitTypeCommand, Response<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public DeleteAdUnitTypeCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            Mediator = mediator;
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            await AdUnitTypeDbRepo.DeleteAsync(request.Id);
            await AdUnitTypeDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
