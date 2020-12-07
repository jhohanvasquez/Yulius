using System.Configuration;

namespace Yulius.Helper.Api
{
    /// <summary>
    /// Llaves con datos (rutas) para consumir servicios
    /// </summary>
    public class KeyConfig_API
    {      

        #region Url Apis Login

        public static bool AutentificadorUniversal = bool.Parse(ConfigurationManager.AppSettings["AutentificadorUniversal"]);

        public static string Url_Login_Register = "Login.Register";
        public static string Url_Login_Auth = "Login.ChangePassword";

        // Codigo del sitio al registrarse en el administrador del autentificador universal.
        public static int? SourceAutentificador = int.Parse(ConfigurationManager.AppSettings["SourceAutentificador"]);
        public static bool? NotifiqueAutentificador = bool.Parse(ConfigurationManager.AppSettings["NotifiqueAutentificador"]);

        #endregion
    }

}
