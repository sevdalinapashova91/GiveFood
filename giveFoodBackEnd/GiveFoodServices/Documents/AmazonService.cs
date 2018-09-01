using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Threading.Tasks;

namespace GiveFoodServices.Documents
{
    public class AmazonService : IAmazonService
    {
        private readonly string BucketName = "givefoodbucket";
        private readonly IAmazonS3 amazonS3Client;

        public AmazonService(IAmazonS3 amozenS3Client)
        {
            this.amazonS3Client = amozenS3Client;
        }

        public async Task<string> UploadFileAsync()
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = BucketName,
                Key = Guid.NewGuid().ToString(),
                ContentBody = "sample text",
                ContentType = "text/plain"
            };

            await amazonS3Client.PutObjectAsync(putRequest);

            return putRequest.Key;
        }

        public async Task<GetObjectResponse> DownloadFileAsync(string keyName)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = BucketName,
                Key = keyName
            };

            ResponseHeaderOverrides responseHeaders = new ResponseHeaderOverrides
            {
                CacheControl = "No-cache",
                ContentDisposition = "attachment; filename=testing.txt"
            };

            request.ResponseHeaderOverrides = responseHeaders;
            return await amazonS3Client.GetObjectAsync(request);
        }
    }


}
