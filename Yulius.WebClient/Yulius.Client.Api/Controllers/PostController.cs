using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yulius.Client.Api.Controllers
{
    public class PostController : Controller
    {
        // GET: Servicio
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult GuadarServicio(string nombre)
        {

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }
    }
}