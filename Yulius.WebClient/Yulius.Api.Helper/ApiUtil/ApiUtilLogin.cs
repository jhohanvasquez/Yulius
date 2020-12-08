using Yulius.Helper.Api;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

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
            public string Token { get; set; }
        }

        /// <summary>
        /// Clase User Login DTO
        /// </summary>
        public class Login_UserDTO
        {
            public string Username { get; set; }
            public string Password { get; set; }
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
                    Identificador = "Login.Auth"
                };

                // Consulta en la base de datos los datos del api.
                api = ApiUtilFactory.API(identificador: api.Identificador);

                // Inicializa la utilidad del api.
                ApiUtil apiUtil = new ApiUtil(api.Uri ?? "", api.DirectorioVirtual ?? "")
                {
                    _url = api.Ruta,
                };

                // Credenciales para solicitar el token.                   
                Login_UserDTO oData = new Login_UserDTO
                {
                    Username = userName,
                    Password = password
                };

                ResponseDTO respuestaToken = await apiUtil.SendRequestPostAsync(oData);

                // Retorno de respuesta
                return new LoginDTO()
                {
                    Token = respuestaToken.Content,
                    StatusCode = respuestaToken.StatusCode,
                    Message = respuestaToken.StatusCode == 200 ? "Inicio de sesión exitoso" : "Error",
                    Error = respuestaToken.StatusCode != 200 ? JsonConvert.DeserializeObject<Api_SetErrorDTO>(respuestaToken.Content) : new Api_SetErrorDTO()
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
                    Identificador = "Login.Register"
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token, model.Email, model.PasswordHash);
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
