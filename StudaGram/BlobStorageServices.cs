using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace StudaGram.Controllers
{
    internal class BlobStorageServices
    {
        private CloudStorageAccount GetStorageAccount()
        {
            ConnectionStringHandler csh = new ConnectionStringHandler();
            return CloudStorageAccount.Parse(/*ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString*/csh.GetAzureStorageConnectionString());
            
        }


        public CloudBlobContainer GetCloudBlobContainer()
        {
            
            var storageAccount = GetStorageAccount();
            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobStorage.GetContainerReference("images");
            if (container.CreateIfNotExists())
            {
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }

            return container;
        }

        public CloudTableClient GetAzureTableAccount()
        {
            var storageAccount = GetStorageAccount();
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient;
        }

        public CloudTable GetTable(CloudTableClient tableClient)
        {
            return tableClient.GetTableReference("Images");
        }
    }
}