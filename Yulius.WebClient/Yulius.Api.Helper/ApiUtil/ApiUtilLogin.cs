using Yulius.Helper.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Utilidades
{
    public class ApiUtilLogin
    {
              
          
        /// <summary>
        /// Clase con el Response estandar de un api
        /// </summary>
        public class Login_ResponseDTO
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }

        /// <summary>
        /// Clase User Login DTO
        /// </summary>
        public class Login_UserDTO
        {
            public int UserId { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public int CountryId { get; set; }
            public string Role { get; set; }
        }

        /// <summary>
        /// Clase para el metodo Register del Login
        /// </summary>
        public class Login_RegisterDTO
        {
            public int UserId { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public int CountryId { get; set; }
            public string Role { get; set; }
        }

        /// <summary>
        /// Clase para retorno del metodo FindByUserName del Login
        /// </summary>
        public class Login_FindByUserName_ResponseDTO : Login_ResponseDTO
        {
            public Login_UserDTO Datos { get; set; }
        }
        

        /// <summary>
        /// Clase para retorno para el inicio de sesion.
        /// </summary>
        public class LoginDTO : Login_ResponseDTO
        {
            public Api_SetErrorDTO Error { get; set; }
        }


        /// <summary>
        /// Consume Metodo que busca un usuario filtrando por UserName
        /// </summary>
        /// <param name="userName">UserName del usuario a buscar</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// Objeto de tipo Login_UserDTO con la informacion del usuario
        /// </returns>
        public async Task<Login_FindByUserName_ResponseDTO> FindByUserName(string userName)
        {
            try
            {
                // Busqueda por userName
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_FindByUserName
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                //Reemplazamos el numero de identificacion.
                apiUtil._url = apiUtil._url.Replace("{userName}", userName);

                // Envia peticion GET
                ResponseDTO response = await apiUtil.SendRequestGetAsync();

                switch (response.StatusCode)
                {
                    case 200:
                        return new Login_FindByUserName_ResponseDTO()
                        {
                            Datos = JsonConvert.DeserializeObject<Login_UserDTO>(response.Content),
                            StatusCode = response.StatusCode,
                            Message = "Ok"
                        };
                    default:
                        if (response.StatusCode != 404)
                        {
                            Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                            new LogUtil().GuardarLog(modulo: "Utilidades > Yulius.Helper.Api > FindByUserName> ", exception: ex);
                        }
                        return new Login_FindByUserName_ResponseDTO()
                        {
                            Datos = null,
                            StatusCode = response.StatusCode,
                            Message = response.Content
                        };
                };
            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > Yulius.Helper.Api > Yulius.Helper.Api_FindByUserName> ", exception: ex);

                return new Login_FindByUserName_ResponseDTO()
                {
                    Datos = null,
                    StatusCode = 500,
                    Message = "Error interno"
                };
            }
        }



        /// <summary>
        /// Realiza el inicio de sesión en el autentificador universar.
        /// </summary>
        /// <param name="userName">UserName del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// Objeto de tipo Api_SetErrorDTO con el error que llega desde el api
        /// </returns>
        public async Task<LoginDTO> Login(string userName, string password)
        {
            try
            {
                // Inicializa el DTO del Api
                ApiDTO api = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_Auth
                };

                // Consulta en la base de datos los datos del api.
                api = ApiUtilFactory.API(identificador: api.Identificador);

                // Inicializa la utilidad del api.
                ApiUtil apiUtil = new ApiUtil(api.Uri ?? "", api.DirectorioVirtual ?? "")
                {
                    _url = api.Ruta,
                };

                // Credenciales para solicitar el token.
                List<KeyValuePair<string, string>> credenciales = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>($"{api.EncabezadoUsuario}", userName),
                    new KeyValuePair<string, string>($"{api.EncabezadoClave}", password)
                };

                // Realiza petición POST
                ResponseDTO response = await apiUtil.SendRequestPostAsync(credenciales, api.RutaToken);

                // Retorno de respuesta
                return new LoginDTO()
                {
                    StatusCode = response.StatusCode,
                    Message = response.StatusCode == 200 ? "Inicio de sesión exitoso" : "Error",
                    Error = response.StatusCode != 200 ? JsonConvert.DeserializeObject<Api_SetErrorDTO>(response.Content) : new Api_SetErrorDTO()
                };
            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > Yulius.Helper.Api > Login", exception: ex);
                return new LoginDTO()
                {
                    StatusCode = 500,
                    Message = "Error interno",
                    Error = new Api_SetErrorDTO()
                };
            }
        }


        /// <summary>
        /// Consume Metodo que registra un usuario en el autentificador universal.
        /// </summary>
        /// <param name="model">Objeto con los datos a registrar</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// </returns>
        public async Task<Login_ResponseDTO> Register(Login_RegisterDTO model)
        {
            try
            {
                // Registrar login universal.
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_Register
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                // Envia peticion POST
                ResponseDTO response = await apiUtil.SendRequestPostAsync(model);

                switch (response.StatusCode)
                {
                    case 200:
                        return new Login_ResponseDTO()
                        {
                            Message = "Ok",
                            StatusCode = response.StatusCode
                        };
                    case 400:
                        return new Login_ResponseDTO()
                        {
                            Message = "Error en los datos subministrados",
                            StatusCode = response.StatusCode
                        };
                    case 401:
                        return new Login_ResponseDTO()
                        {
                            Message = "Error en el token de acceso",
                            StatusCode = response.StatusCode
                        };
                    default:
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > Yulius.Helper.Api > Yulius.Helper.Api_Register> ", exception: ex);

                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                }

            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > Yulius.Helper.Api > Yulius.Helper.Api_Register> ", exception: ex);
                return new Login_ResponseDTO()
                {
                    Message = "Error del sistema",
                    StatusCode = 404
                };
            }
        }


    }
}
