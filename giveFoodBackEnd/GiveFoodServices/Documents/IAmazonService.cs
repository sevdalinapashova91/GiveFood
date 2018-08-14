namespace GiveFoodServices.Documents
{
    public interface IAmazonService
    {
        void DownloadFile();
        void UploadFile(string filePath);
    }
}