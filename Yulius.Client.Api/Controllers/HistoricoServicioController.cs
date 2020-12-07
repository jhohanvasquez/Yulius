using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yulius.Client.Api.ServiceTest;

namespace Yulius.Client.Api.Controllers
{
    public class HistoricoServicioController : Controller
    {
        // GET: HistoricoServicio
        public ActionResult Index()
        {
            using (var oService = new ServiceTestClient())
            {
                var list = oService.ListarSocios().ToList();
                var listServcios = oService.ListarServicios().ToList();
                ViewBag.ListSocios = new SelectList(list, "idSocio", "nombre");
                ViewBag.ListServicios = new SelectList(listServcios, "idServicio", "servicio1");
                ViewBag.Historial = oService.ListarHistoricoServicio().ToList();
                return View();
            }
        }

        [HttpPost, ValidateHeaderAntiForgeryToken]
        public JsonResult GuadarHistorico(int idSocio, int idServicio, DateTime fechaUso)
        {
            using (ServiceTestClient oService = new ServiceTestClient())
            {
                oService.GuardarHistoricoServicio(idServicio, idSocio);
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
        }
    }
}