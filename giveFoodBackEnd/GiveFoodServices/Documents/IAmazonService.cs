using Amazon.S3.Model;
using System.Threading.Tasks;

namespace GiveFoodServices.Documents
{
    public interface IAmazonService
    {
        Task<GetObjectResponse> DownloadFileAsync(string keyName);

        Task<string> UploadFileAsync(string filePath);
    }
}