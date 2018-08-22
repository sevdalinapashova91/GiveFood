using Amazon.S3.Model;
using System.Threading.Tasks;

namespace GiveFoodServices.Documents
{
    public interface IAmazonService
    {
        Task<GetObjectResponse> DownloadFile(string keyName);

        Task<string> UploadFile();
    }
}