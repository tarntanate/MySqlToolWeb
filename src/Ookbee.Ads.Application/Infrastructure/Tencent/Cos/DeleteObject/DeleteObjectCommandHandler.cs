using COSXML.CosException;
using COSXML.Model.Object;
using MediatR;
using Ookbee.Ads.Application.Infrastructure.Tencent.Cos.InitializeCosXmlServer;
using Ookbee.Ads.Common.Result;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Infrastructure.Tencent.Cos.DeleteObject
{
    public class DeleteObjectCommandHandler : IRequestHandler<DeleteObjectCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }

        public DeleteObjectCommandHandler(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<HttpResult<bool>> Handle(DeleteObjectCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();
            try
            {
                var cosXml = await Mediator.Send(new InitializeCosXmlServerCommand(), cancellationToken);
                var deleteObjectRequest = new DeleteObjectRequest(request.Bucket, request.Key);
                var deleteObjectResult = cosXml.DeleteObject(deleteObjectRequest);
                if (deleteObjectResult.httpCode == 200)
                    return result.Success(true);
                return result.Fail(deleteObjectResult.httpCode, deleteObjectResult.httpMessage);
            }
            catch (CosServerException serverEx)
            {
                return result.Fail(serverEx.statusCode, serverEx.errorMessage);
            }
        }
    }
}
