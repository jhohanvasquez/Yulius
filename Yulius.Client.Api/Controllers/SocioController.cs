using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yulius.Client.Api.ServiceTest;

namespace Yulius.Client.Api.Controllers
{
    public class SocioController : Controller
    {
        // GET: Socio
        public ActionResult Index()
        {
                return View();
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult GuadarSocio(string nombre)
        {
            using (var oService = new ServiceTestClient())
            {
                oService.GuardarSocio(nombre);
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
        }
    }
}