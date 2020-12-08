using Antlr.Runtime;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yulius.Helper.Api;
using Yulius.Helper.Api.Datos;
using static Utilidades.ApiUtilLogin;

namespace Utilidades
{
    /// <summary>
    /// Gestión de las instancias de ApiUtil dependiendo del tipo de seguridad.
    /// </summary>
    /// <remarks>Se usa el patrón de diseño Simple Factory.</remarks>
    public static class ApiUtilFactory
    {
        /// <summary>
        /// Obtiene la instancia de ApiUtil.
        /// </summary>
        /// <param name="api">Credenciales de usuario necesarios según el tipo de seguridad.</param>
        /// <returns>Instancia de ApiUtil</returns>
        public static async Task<ApiUtil> GetApiUtil(ApiDTO api, string userLogin, string passwordLogin)
        {

            string parametros = api.Parametros;
            api = API(identificador: api.Identificador);

            if (!string.IsNullOrEmpty(parametros))
            {
                api.Ruta = string.Format(api.Ruta, new StringListasUtil().ConvertirStringEnLista(parametros));
            }
            ApiUtil apiUtil = new ApiUtil(api.Uri ?? "", api.DirectorioVirtual ?? "")
            {
                _url = api.Ruta,
            };

            switch (api.TipoSeguridad)
            {
                case TipoSeguridad.Simple:
                    break;
                case TipoSeguridad.Token:
                    apiUtil.LoadHeaders($"{api.EncabezadoToken}", $"{api.Prefijo?.Trim()}  {api.Token}");
                    break;
                case TipoSeguridad.UserPass:
                    apiUtil.LoadHeaders(new Dictionary<string, string>
                    {
                        { $"{api.EncabezadoUsuario}", api.Usuario},
                        { $"{api.EncabezadoClave}", api.Clave}
                    });
                    break;
                case TipoSeguridad.UserPassToken:
                    // Credenciales para solicitar el token.                   
                    Login_UserDTO oData = new Login_UserDTO();
                    oData.Username = userLogin;
                    oData.Password = passwordLogin;

                    var respuestaToken = await apiUtil.SendRequestPostAsync(oData);
                    if (respuestaToken.StatusCode == 200)
                    {
                        TokenDTO token = JsonConvert.DeserializeObject<TokenDTO>(respuestaToken.Content);
                        apiUtil.LoadHeaders($"{api.EncabezadoToken}", $"{api.Prefijo?.Trim()}  {token.Access_token}");
                    }
                    break;
                case TipoSeguridad.All:
                    break;
                default:
                    break;
            }
            return apiUtil;
        }

        /// <summary>
        /// Datos del api
        /// </summary>
        /// <returns>Datos del api</returns>
        public static ApiDTO API(string identificador)
        {
            using (BDYulius contexto = new BDYulius())
            {
                Yulius.Api.Helper.Datos.pa_Api_Informacion_Result lista = contexto.pa_Api_Informacion(pidentificador: identificador).FirstOrDefault();
                IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<Yulius.Api.Helper.Datos.pa_Api_Informacion_Result, ApiDTO>()).CreateMapper();
                return mapper.Map<ApiDTO>(lista);
            }
        }

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

    /// <summary>
    /// Clase Response
    /// </summary>
    public class ResponseDTO
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }

        public static implicit operator Tuple<object, object>(ResponseDTO v)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Clase SetError
    /// </summary>
    public class Api_SetErrorDTO
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }


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



}