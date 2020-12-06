using System.Configuration;

namespace LoginApi
{
    /// <summary>
    /// Llaves con datos (rutas) para consumir servicios
    /// </summary>
    public class KeyConfig_API
    {      

        #region Url Apis Login

        public static bool AutentificadorUniversal = bool.Parse(ConfigurationManager.AppSettings["AutentificadorUniversal"]);

        public static string Url_Login_Register = "Login.Register";
        public static string Url_Login_ConfirmEmail = "Login.ConfirmEmail";
        public static string Url_Login_SendConfirmCode = "Login.SendConfirmCode";
        public static string Url_Login_ChangePassword = "Login.ChangePassword";
        public static string Url_Login_ForgotPassword = "Login.ForgotPassword";
        public static string Url_Login_ResetPassword = "Login.ResetPassword";
        public static string Url_Login_Cliente = "Login.Cliente";
        public static string Url_Login_FindByUserName = "Login.FindByUserName";
        public static string Url_Login_GetOTP = "Login.GetOTP";
        public static string Url_Login_DeleteOTP = "Login.DeleteOTP";
        public static string Url_Login_Auth = "Login.ChangePassword";

        // Codigo del sitio al registrarse en el administrador del autentificador universal.
        public static int? SourceAutentificador = int.Parse(ConfigurationManager.AppSettings["SourceAutentificador"]);
        public static bool? NotifiqueAutentificador = bool.Parse(ConfigurationManager.AppSettings["NotifiqueAutentificador"]);

        #endregion
    }

}
