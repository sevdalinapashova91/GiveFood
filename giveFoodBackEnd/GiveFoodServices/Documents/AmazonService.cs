
namespace GiveFoodServices.Documents
{
    public class AmazonService : IAmazonService
    {
        private readonly string BucketName = "givefoodbucket";
        public readonly string KeyName = "";

        public void UploadFile(string filePath)
        {
            //try
            //{
            //    TransferUtility fileTransferUtility = new
            //        TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.EUCentral1));

            //    // 1. Upload a file, file name is used as the object key name.
            //    fileTransferUtility.Upload(filePath, BucketName);

               
            //}
            //catch (AmazonS3Exception s3Exception)
            //{

          //  }
        }
        public void DownloadFile()
        {
        }
    }

   
}
