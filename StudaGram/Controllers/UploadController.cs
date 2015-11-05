using System;
using System.Collections.Generic;
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
            CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                blobs.Add(blobItem.Uri.ToString());
            }

            return View(blobs);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase Image)
        {
            try
            {
                if (Image.ContentLength > 0)
                {
                    CloudBlobContainer blobContainer = _blobStorageService.GetCloudBlobContainer();
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(Image.FileName);
                    blob.UploadFromStream(Image.InputStream);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Processor Usage" + ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}