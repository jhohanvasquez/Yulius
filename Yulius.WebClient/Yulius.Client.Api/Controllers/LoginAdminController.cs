using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Utilidades;
using Yulius.Client.Api.Filters;
using static System.Web.Security.FormsAuthentication;

namespace Yulius.Client.Api.Controllers
{
    public class LoginAdminController : Controller
    {
        private ApiUtilLogin oApilogin = new ApiUtilLogin();
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
        public JsonResult ValidateAdmin(string usu, string pass)
        {
            var resultTask = oApilogin.Login(usu, pass);
            resultTask.Wait();
            if (resultTask.IsCompleted && resultTask.Result != null)
            {
                Session["LogonAdmin"] = usu;
            }
            return Json(resultTask, JsonRequestBehavior.AllowGet);
        }
    }
}

