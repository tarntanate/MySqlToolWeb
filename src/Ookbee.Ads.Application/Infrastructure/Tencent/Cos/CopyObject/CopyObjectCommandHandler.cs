using COSXML.CosException;
using COSXML.Model.Object;
using COSXML.Model.Tag;
using COSXML.Utils;
using MediatR;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.InitializeCosXmlServer;
using Ookbee.Ads.Common.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos.CopyObject
{
    public class CopyObjectCommandHandler : IRequestHandler<CopyObjectCommand, Response<bool>>
    {
        private readonly IMediator Mediator;

        public CopyObjectCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Response<bool>> Handle(CopyObjectCommand request, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();
            try
            {
                var cosXml = await Mediator.Send(new InitializeCosXmlServerCommand(), cancellationToken);
                var sourceAppid = request.SourceAppid;
                var sourceRegion = request.SourceRegion;
                var sourceBucket = request.SourceBucket;
                var sourceKey = request.SourceKey;
                var copySourceStruct = new CopySourceStruct(sourceAppid, sourceBucket, sourceRegion, sourceKey);
                var copyObjectRequest = new CopyObjectRequest(request.DestinationBucket, request.DestinationKey);
                copyObjectRequest.SetSign(TimeUtils.GetCurrentTime(TimeUnit.Seconds), 600);
                copyObjectRequest.SetCopySource(copySourceStruct);
                copyObjectRequest.SetCopyMetaDataDirective(COSXML.Common.CosMetaDataDirective.Copy);
                var copyObjectResult = cosXml.CopyObject(copyObjectRequest);
                if (copyObjectResult.httpCode == 200)
                    return result.OK(true);
                return result.Status((HttpStatusCode)copyObjectResult.httpCode, copyObjectResult.httpMessage);
            }
            catch (CosServerException serverEx)
            {
                return result.Status((HttpStatusCode)serverEx.statusCode, serverEx.errorMessage);
            }
        }
    }
}
