using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;

namespace StudaGram.Controllers
{
    public class UploadController : Controller
    {
        BlobStorageServices _blobStorageService = new BlobStorageServices();

        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase Image)
        {
            CloudBlobContainer container = _blobStorageService.GetCloudBlobContainer();
            string path = @"C:\Temp";

            var image = Request.Files["image"];
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
            }
            return View();
        }
    }
}