using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudaGram.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            //file.SaveAs(Server.MapPath("~/App_Data/Test.jpg"));
            Request.Files["Upload"].SaveAs(Server.MapPath("~/App_Data/Test.jpg"));
            return View();
        }
    }
}