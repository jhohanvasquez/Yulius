using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LoginApi
{
    #region Administrador Api

    public partial class ApiDTO
    {
        public TipoSeguridad TipoSeguridad { get; set; }
        public long Id { get; set; }
        public string Uri { get; set; }
        public string Nombre { get; set; }
        public string Token { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string DirectorioVirtual { get; set; }
        public bool Visual { get; set; }
        public string RutaToken { get; set; }
        public string EncabezadoToken { get; set; }
        public string EncabezadoUsuario { get; set; }
        public string EncabezadoClave { get; set; }
        public string Prefijo { get; set; }
        public string Ruta { get; set; }
        public string Identificador { get; set; }
        public string Parametros { get; set; }
    }


    /// <summary>
    /// Tipos de seguridad del servicio.
    /// </summary>
    public enum TipoSeguridad
    {
        Simple,
        Token,
        UserPass,
        UserPassToken,
        All
    }


    public partial class ApiConfiguraciones_RutasDTO
    {
        public long Id { get; set; }
        public long IdApi { get; set; }
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        public string Identificador { get; set; }
        public string RealizadaPor { get; set; }
    }

    /// <summary>
    /// Clase Token
    /// </summary>
    public class TokenDTO
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Expires_in { get; set; }
    }

    #endregion   

    #region Api Login


    public class LoginApi_ErroresDTO
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }

    #endregion
}
