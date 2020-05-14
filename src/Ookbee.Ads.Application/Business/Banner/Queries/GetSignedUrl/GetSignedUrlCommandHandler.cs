﻿using MediatR;
using Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetSignedUrlCommand
{
    public class GetSignedUrlCommandHandler : IRequestHandler<GetSignedUrlCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private OokbeeAdsMongoDBRepository<UploadUrlDocument> UploadUrlMongoDB { get; }

        public GetSignedUrlCommandHandler(
            IMediator mediator,
            OokbeeAdsMongoDBRepository<UploadUrlDocument> uploadUrlMongoDB)
        {
            Mediator = mediator;
            UploadUrlMongoDB = uploadUrlMongoDB;
        }

        public async Task<HttpResult<string>> Handle(GetSignedUrlCommand request, CancellationToken cancellationToken)
        {
            var cosConfig = GlobalVar.AppSettings.Tencent.Cos;
            var signedUrl = await Mediator.Send(new GenerateUploadUrlCommand()
            {
                Bucket = cosConfig.Bucket.Banner,
                Key = $"{request.FileName}{request.FileExtension}"
            });
            return signedUrl;
        }
    }
}
