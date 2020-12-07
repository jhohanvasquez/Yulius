using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Yulius.Client.Api.ServiceTest;
using static System.Web.Security.FormsAuthentication;

namespace Yulius.Client.Api.Controllers
{
    public class LoginAdminController : Controller
    {
        // GET: LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usu"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public JsonResult ValidateAdmin(string usu)
        {
            bool result = false;

            using (ServiceTestClient oService = new ServiceTestClient())
            {
                result = oService.GetUser(usu);
                if (result)
                {
                    Session["LogonAdmin"] = usu;
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

