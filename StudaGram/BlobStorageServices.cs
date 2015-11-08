using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace StudaGram.Controllers
{
    internal class BlobStorageServices
    {
        public CloudBlobContainer GetCloudBlobContainer()
        {
            ConnectionStringHandler csh = new ConnectionStringHandler();
            var storageAccount = CloudStorageAccount.Parse(/*ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString*/csh.GetAzureSqlConnectionString());
            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobStorage.GetContainerReference("testcontainer");
            if (container.CreateIfNotExists())
            {
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }

            return container;
        }
    }
}