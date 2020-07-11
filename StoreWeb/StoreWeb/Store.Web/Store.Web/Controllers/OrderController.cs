using Store.Web.StoreWS;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Import(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0 )
            {
                string filePath = Path.Combine(Server.MapPath("~/TempImportFiles"), Path.GetFileName(file.FileName));
                
                try
                {
                    file.SaveAs(filePath);

                    var client = new ServiceSoapClient();
                    client.ImportFile(filePath);

                    ViewBag.FileStatus = "File uploaded successfully.";
                    ViewBag.FileStatusClass = "text-success";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading.";
                    ViewBag.FileStatusClass = "text-danger";
                }
            }

            return View();
        }
    }
}