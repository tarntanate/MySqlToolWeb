using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommandHandler : IRequestHandler<DeleteAdUnitTypeCommand, Response<bool>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public DeleteAdUnitTypeCommandHandler(
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            await AdUnitTypeDbRepo.DeleteAsync(request.Id);
            await AdUnitTypeDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
