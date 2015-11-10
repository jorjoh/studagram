using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using StudaGram.Models;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage.Table;

namespace StudaGram.Controllers
{
    public class UploadController : Controller
    {
        BlobStorageServices _blobStorageService = new BlobStorageServices();

        // GET: Upload
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Home/AccessDenied");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(UploadModel Image)
        {
            CloudBlobContainer container = _blobStorageService.GetCloudBlobContainer();
            string path = @"C:\Temp";

            var image = Image.UploadedFile;
            if (image == null)
            {
                Response.Write("Failed to upload image");
            }
            else
            {
                Response.Write(String.Format("Got image {0} of type {1} and size {2}", image.FileName,
                    image.ContentType, image.ContentLength));

                string uniqueBlobName = string.Format("image_{0}-{1}", Guid.NewGuid().ToString(),
                    Path.GetExtension(image.FileName));
                CloudBlockBlob blob = container.GetBlockBlobReference(uniqueBlobName);
                blob.Properties.ContentType = image.ContentType;
                blob.UploadFromStream(image.InputStream);

                var result = UploadedImage.create();
                result.Url = blob.Uri.ToString();
                result.Title = Image.Title;
                result.Description = Image.Description;
                result.Uploaded = DateTime.Now;
                result.Owner = User.Identity.GetUserId();
                result.Likes = 0;

                var tableClient = _blobStorageService.GetAzureTableAccount();
                var table = _blobStorageService.GetTable(tableClient);
                table.CreateIfNotExists();
                var tableOperation = TableOperation.Insert(result);
                table.Execute(tableOperation);       
            }
            return View();
        }
    }
}