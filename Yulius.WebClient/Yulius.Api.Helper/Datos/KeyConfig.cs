using System.Configuration;

namespace Yulius.Helper.Api
{
    /// <summary>
    /// Llaves con datos (rutas) para consumir servicios
    /// </summary>
    public class KeyConfig_API
    {      

        #region Apis Login
        public static bool AutentificadorUniversal = bool.Parse(ConfigurationManager.AppSettings["AutentificadorUniversal"]);
        // Codigo del sitio al registrarse en el administrador del autentificador universal.
        public static int? SourceAutentificador = int.Parse(ConfigurationManager.AppSettings["SourceAutentificador"]);
        public static bool? NotifiqueAutentificador = bool.Parse(ConfigurationManager.AppSettings["NotifiqueAutentificador"]);

        #endregion
    }

}
