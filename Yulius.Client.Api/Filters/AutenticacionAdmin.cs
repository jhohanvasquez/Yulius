
using System;
using Yulius.Client.Api.Controllers;

namespace Yulius.Client.Api.Filters
{
    public class AutenticacionAdmin : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext Contexto)
        {
            try
            {
                if (Contexto.HttpContext.Session == null || string.IsNullOrEmpty(Convert.ToString(Contexto.HttpContext.Session["LogonAdmin"])))
                {
                    UtilController.Exit(Contexto);
                }
                else
                {
                    base.OnActionExecuting(Contexto);
                }
            }
            catch
            {
                UtilController.Exit(Contexto);
            }
        }
    }
}
