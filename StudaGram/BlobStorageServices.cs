using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace StudaGram.Controllers
{
    internal class BlobStorageServices
    {
        private string AzureStorageConnectionString = System.Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        public CloudBlobContainer GetCloudBlobContainer()
        {
            if (AzureStorageConnectionString == null)
            {
                AzureStorageConnectionString = "UseDevelopmentStorage=true;";
            }

            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(AzureStorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("images");
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            return blobContainer;
        }
    }
}