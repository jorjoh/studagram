using Microsoft.WindowsAzure.Storage.Table;
using StudaGram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudaGram.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BlobStorageServices _blobStorageService = new BlobStorageServices();
            var tableClient = _blobStorageService.GetAzureTableAccount();
            var table = _blobStorageService.GetTable(tableClient);
            var query = new TableQuery<UploadedImage>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Images"));
            var model = new HomeViewModel() { Images = table.ExecuteQuery(query).ToList() };

            return View(model);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AccessDenied()
        {
            ViewBag.Message = "Access Denied";

            return View();
        }
    }
}