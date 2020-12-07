using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yulius.Client.Api.Controllers
{
    public class UtilController : Controller
    {

        // GET: Utilidades

            /// <summary>
            /// 
            /// </summary>
            /// <param name="Contexto"></param>
        public static void Exit(System.Web.Mvc.ActionExecutingContext Contexto)
        {
            Contexto.HttpContext.Session.Clear();
            Contexto.HttpContext.Session.Abandon();
            Contexto.Result = new System.Web.Mvc.EmptyResult();
            System.Web.Mvc.UrlHelper UrlTmp = new System.Web.Mvc.UrlHelper(Contexto.RequestContext);
            String Url = UrlTmp.Action("SalidaSegura", "Home");
            if (Url != null) Contexto.HttpContext.Response.Redirect(Url, false);
        }


    }
}