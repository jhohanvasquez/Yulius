using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yulius.Client.Api.ServiceTest;

namespace Yulius.Client.Api.Controllers
{
    public class ServicioController : Controller
    {
        // GET: Servicio
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult GuadarServicio(string nombre)
        {
            using (ServiceTestClient oService = new ServiceTestClient())
            {
                oService.GuardarServicio(nombre);
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
        }
    }
}