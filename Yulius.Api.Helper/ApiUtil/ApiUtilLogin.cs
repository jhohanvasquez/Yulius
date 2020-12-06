using LoginApi;
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
        /// Clase User Cliente DTO
        /// </summary>
        public class Cliente_UserDTO
        {
            public string Cedula { get; set; }
            public string FechaExpedicionCedula { get; set; }
            public string NombreCompleto { get; set; }
            public string Sede { get; set; }
            public string Email { get; set; }
            public string NumeroCelular { get; set; }
        }

        /// <summary>
        /// Clase User Login DTO
        /// </summary>
        public class Login_UserDTO
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string IdentificationNumber { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public bool? EmailConfirmed { get; set; }
            public string PhoneNumber { get; set; }
            public string Campus { get; set; }
            public bool Active { get; set; }
            public string Rol { get; set; }
            public bool? ChangePassword { get; set; }
            public DateTime? LastPasswordChangedDate { get; set; }
            public string OTP { get; set; }
            public DateTime? OTPExpireDate { get; set; }
        }

        /// <summary>
        /// Clase para el metodo Register del Login
        /// </summary>
        public class Login_RegisterDTO
        {
            public string User;

            public string IdentificationNumber { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Campus { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public bool? Notifique { get; set; }
            public int? Source { get; set; }
        }

        /// <summary>
        /// Clase para el metodo Confirm del Login
        /// </summary>
        public class Login_ConfirmDTO
        {
            public string User { get; set; }
            public string Code { get; set; }
        }

        /// <summary>
        /// Clase para el metodo SendConfirmCode del Login
        /// </summary>
        public class Login_SendConfirmCodeDTO
        {
            public string User { get; set; }
            public bool? Notifique { get; set; }
            public int? Source { get; set; }
        }

        /// <summary>
        /// Clase para el metodo ForgotPassword del Login
        /// </summary>
        public class Login_ForgotPasswordDTO
        {
            public string User { get; set; }
            public bool? Notifique { get; set; }
            public int? Source { get; set; }
        }

        /// <summary>
        /// Clase para el metodo ChangePassword del Login
        /// </summary>
        public class Login_ResetPasswordDTO
        {
            public string Email;

            public string User { get; set; }
            public string Code { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        /// <summary>
        /// Clase para el metodo ChangePassword del Login
        /// </summary>
        public class Login_ChangePasswordDTO
        {
            public string User { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }

        /// <summary>
        /// Clase para retorno del metodo FindByUserName del Login
        /// </summary>
        public class Login_FindByUserName_ResponseDTO : Login_ResponseDTO
        {
            public Login_UserDTO Datos { get; set; }
        }

        /// <summary>
        /// Clase para retorno del metodo GetClienteById del Login
        /// </summary>
        public class Login_GetClienteById_ResponseDTO : Login_ResponseDTO
        {
            public Cliente_UserDTO Datos { get; set; }
        }

        /// <summary>
        /// Clase para retorno del metodo GetOTP
        /// </summary>
        public class Login_GetOTP_ResponseDTO : Login_ResponseDTO
        {
            public string OTP { get; set; }
        }

        /// <summary>
        /// Clase para retorno del metodo ForgotPassword
        /// </summary>
        public class Login_ForgotPassword_ResponseDTO : Login_ResponseDTO
        {
            public string CodigoOTP { get; set; }
        }

        /// <summary>
        /// Clase para retorno del metodo SendConfirmCode
        /// </summary>
        public class Login_SendConfirmCode_ResponseDTO : Login_ResponseDTO
        {
            public string CodigoOTP { get; set; }
        }


        /// <summary>
        /// Clase para retorno para el inicio de sesion.
        /// </summary>
        public class LoginDTO : Login_ResponseDTO
        {
            public Api_SetErrorDTO Error { get; set; }
        }


        /// <summary>
        /// Consume el api del cliente
        /// </summary>
        /// <param name="identification">Identificación a buscar</param>
        /// <returns></returns>
        public async Task<Cliente_UserDTO> Api_Cliente(string identification)
        {
            try
            {
                // Consumir Api del cliente
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_Cliente
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                //Reemplazamos el numero de identificacion.
                apiUtil._url = apiUtil._url.Replace("{Identificador}", identification);

                // Captura respuesta
                Cliente_UserDTO Data = new Cliente_UserDTO();

                // Envia peticion GET
                ResponseDTO response = await apiUtil.SendRequestGetAsync();
                switch (response.StatusCode)
                {
                    case 200:
                        Data = JsonConvert.DeserializeObject<List<Cliente_UserDTO>>(response.Content).FirstOrDefault();
                        return Data;
                    case 400:
                        return null;
                    default:
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > Api_Cliente ", exception: ex);
                        return null;
                }

            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > Api_Cliente ", exception: ex);
                return null;
            }

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
                            new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > FindByUserName> ", exception: ex);
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
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_FindByUserName> ", exception: ex);

                return new Login_FindByUserName_ResponseDTO()
                {
                    Datos = null,
                    StatusCode = 500,
                    Message = "Error interno"
                };
            }
        }


        /// <summary>
        /// Consume Metodo que Confirma y activa la cuenta.
        /// </summary>
        /// <param name="model">Objeto con el user y el code de confirmación</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// </returns>
        public async Task<Login_ResponseDTO> Confirm(Login_ConfirmDTO model)
        {
            try
            {
                // Confirmar cuenta
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_ConfirmEmail
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                // Envia peticion POST
                ResponseDTO response = await apiUtil.SendRequestPostAsync(model);

                // Valida la respuesta del api y retorna DTO personalizado.
                switch (response.StatusCode)
                {
                    case 200:
                        return new Login_ResponseDTO()
                        {
                            Message = "Email confirmado exitosamente.",
                            StatusCode = response.StatusCode
                        };
                    case 400:
                        return new Login_ResponseDTO()
                        {
                            Message = "Error en los datos subministrados",
                            StatusCode = response.StatusCode
                        };
                    default:
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_ConfirmEmail> ", exception: ex);
                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                };

            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_ConfirmEmail> ", exception: ex);
                return new Login_ResponseDTO()
                {
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
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > Login", exception: ex);
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
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_Register> ", exception: ex);

                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                }

            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_Register> ", exception: ex);
                return new Login_ResponseDTO()
                {
                    Message = "Error del sistema",
                    StatusCode = 404
                };
            }
        }


        /// <summary>
        /// Consulta el api del cliente desde el autentificador universar filtrando por la identificacion.
        /// </summary>
        /// <param name="identification">identificador a filtrar</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// Objeto de tipo Cliente_UserDTO con la informacion del api del cliente
        /// </returns>
        public async Task<Login_GetClienteById_ResponseDTO> GetClienteById(string identification)
        {
            try
            {
                // Consulta el api del cliente desde el autentificador universar filtrando por la identificacion.
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_Cliente
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                //Reemplazamos el numero de identificacion.
                apiUtil._url = apiUtil._url.Replace("{Identificador}", identification);

                // Parametros Respuesta
                Cliente_UserDTO data = new Cliente_UserDTO();
                string message;

                // Envia peticion GET
                ResponseDTO response = await apiUtil.SendRequestGetAsync();

                // Se administra la respuesta del api.
                switch (response.StatusCode)
                {
                    case 200:
                        data = JsonConvert.DeserializeObject<List<Cliente_UserDTO>>(response.Content).FirstOrDefault();
                        message = "Ok";
                        break;
                    case 401:
                        message = "No Autorizado. No se ha indicado o es incorrecto el Token de acceso";
                        break;
                    case 404:
                        message = "No Encontrado. No se ha encontrado el cliente solicitado";
                        break;
                    default:
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > GetClienteById ", exception: ex);
                        message = response.Content;
                        break;
                }

                // Retorno
                return new Login_GetClienteById_ResponseDTO()
                {
                    Datos = data,
                    Message = message,
                    StatusCode = response.StatusCode
                };
            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > GetClienteById ", exception: ex);

                // Retorno
                return new Login_GetClienteById_ResponseDTO()
                {
                    Datos = null,
                    Message = "Error interno",
                    StatusCode = 500
                };
            }

        }


        /// <summary>
        /// Consume Metodo que realiza la solicitud del código de confirmación de cuenta.
        /// </summary>
        /// <param name="model">Objeto con los datos para la petición</param>
        /// <returns>        
        /// StatusCode de la petición
        /// Message que responde el api
        /// </returns>
        public async Task<Login_SendConfirmCode_ResponseDTO> SendConfirmCode(Login_SendConfirmCodeDTO model)
        {
            try
            {
                // Enviar codigo de confirmación
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_SendConfirmCode
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                // Envia peticion POST
                ResponseDTO response = await apiUtil.SendRequestPostAsync(model);

                // Valida la respuesta del api y retorna DTO personalizado.
                switch (response.StatusCode)
                {
                    case 200:
                        return new Login_SendConfirmCode_ResponseDTO()
                        {
                            CodigoOTP = response.Content,
                            Message = "Se envió el código de confirmación a su correo electrónico.",
                            StatusCode = response.StatusCode
                        };
                    case 400:
                        return new Login_SendConfirmCode_ResponseDTO()
                        {
                            Message = "Error. Los datos subministrados no son correctos.",
                            StatusCode = response.StatusCode
                        };
                    case 401:
                        return new Login_SendConfirmCode_ResponseDTO()
                        {
                            Message = "No Autorizado. No se ha indicado o es incorrecto el Token de acceso.",
                            StatusCode = response.StatusCode
                        };
                    default:
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > SendConfirmCode", exception: ex);
                        return new Login_SendConfirmCode_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                };

            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > SendConfirmCode", exception: ex);
                return new Login_SendConfirmCode_ResponseDTO()
                {
                    StatusCode = 500,
                    Message = "Error interno"
                };
            }
        }


        /// <summary>
        /// Consume Metodo que permite cambiar la contraseña.
        /// </summary>
        /// <param name="model">Objeto con los datos para la petición</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// </returns>
        public async Task<Login_ResponseDTO> ChangePassword(Login_ChangePasswordDTO model)
        {
            try
            {
                // Cambio de Contraseña
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_ChangePassword
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                // Envia peticion POST
                ResponseDTO response = await apiUtil.SendRequestPostAsync(model);

                // Valida la respuesta del api y retorna DTO personalizado.
                switch (response.StatusCode)
                {
                    case 200:
                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                    case 400:
                        return new Login_ResponseDTO()
                        {
                            Message = "Error. Los datos subministrados no son correctos.",
                            StatusCode = response.StatusCode
                        };
                    case 401:
                        return new Login_ResponseDTO()
                        {
                            Message = "No Autorizado. No se ha indicado o es incorrecto el Token de acceso.",
                            StatusCode = response.StatusCode
                        };
                    default:
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > ChangePassword", exception: ex);
                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                };

            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > ChangePassword", exception: ex);
                return new Login_ResponseDTO()
                {
                    StatusCode = 500,
                    Message = "Error interno"
                };
            }
        }


        /// <summary>
        /// Consume Metodo que permite cambiar la contraseña.
        /// </summary>
        /// <param name="model">Objeto con los datos para la petición</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// </returns>
        public async Task<Login_ResponseDTO> ResetPassword(Login_ResetPasswordDTO model)
        {
            try
            {
                // Cambio de Contraseña
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_ResetPassword
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                // Envia peticion POST
                ResponseDTO response = await apiUtil.SendRequestPostAsync(model);

                // Valida la respuesta del api y retorna DTO personalizado.
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
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                    case 401:
                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                    case 404:
                        return new Login_ResponseDTO()
                        {
                            Message = "Usuario no encontrado.",
                            StatusCode = response.StatusCode
                        };
                    default:
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > ResetPassword", exception: ex);
                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                };

            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > ResetPassword", exception: ex);
                return new Login_ResponseDTO()
                {
                    StatusCode = 500,
                    Message = "Error interno"
                };
            }
        }


        /// <summary>
        /// Consume metodo que realiza la solicitud de recuperación de clave.
        /// </summary>
        /// <param name="model">Objeto con los datos para la petición</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// </returns>
        public async Task<Login_ForgotPassword_ResponseDTO> ForgotPassword(Login_ForgotPasswordDTO model)
        {
            try
            {
                // Solicitar asociado
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_ForgotPassword
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                // Envia peticion POST
                ResponseDTO response = await apiUtil.SendRequestPostAsync(model);

                // Valida la respuesta del api y retorna DTO personalizado.
                switch (response.StatusCode)
                {
                    case 200:
                        return new Login_ForgotPassword_ResponseDTO()
                        {
                            CodigoOTP = response.Content,
                            Message = "Ok",
                            StatusCode = response.StatusCode
                        };
                    case 400:
                        return new Login_ForgotPassword_ResponseDTO()
                        {
                            Message = response.Content.Replace("\"", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty),
                            StatusCode = response.StatusCode
                        };
                    default:
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > ForgotPassword", exception: ex);
                        return new Login_ForgotPassword_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                };

            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > ForgotPassword", exception: ex);
                return new Login_ForgotPassword_ResponseDTO()
                {
                    StatusCode = 500,
                    Message = "Error interno"
                };
            }
        }


        /// <summary>
        /// Consume metodo que elimina el codigo OTP del autentificador universal.
        /// </summary>
        /// <param name="userName">UserName del usuario al cual se le eliminara el codigo OTP</param>
        /// <returns>
        /// StatusCode de la petición
        /// Message que responde el api
        /// </returns>
        public async Task<Login_ResponseDTO> DeleteOTP(string userName)
        {
            try
            {
                // Busqueda por userName
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_DeleteOTP
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                //Reemplazamos el numero de identificacion.
                apiUtil._url = apiUtil._url.Replace("{userName}", userName);

                // Envia peticion DELETE
                ResponseDTO response = await apiUtil.SendRequestDeleteAsync();

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
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                    case 401:
                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                    case 404:
                        return new Login_ResponseDTO()
                        {
                            Message = "Usuario no encontrado.",
                            StatusCode = response.StatusCode
                        };
                    default:
                        if (response.StatusCode != 404)
                        {
                            Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                            new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_DeleteOTP> ", exception: ex);
                        }
                        return new Login_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                };
            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_DeleteOTP> ", exception: ex);

                return new Login_ResponseDTO()
                {
                    StatusCode = 500,
                    Message = "Error interno"
                };
            }
        }


        /// <summary>
        /// Consume metodo que obtiene el codigo OTP del autentificador universal.
        /// </summary>
        /// <param name="userName">UserName del usuario al cual se le eliminara el codigo OTP</param>
        /// <returns>
        /// StatusCode de la petición
        /// OTP = Codigo Entregado por el api
        /// Message que responde el api
        /// </returns>
        public async Task<Login_GetOTP_ResponseDTO> GetOTP(string userName, bool notifique)
        {
            try
            {
                // Busqueda por userName
                ApiDTO api_token = new ApiDTO
                {
                    // Identificador ruta
                    Identificador = KeyConfig_API.Url_Login_GetOTP
                };

                // Consulta en base de datos los datos del api y ejecuta el nivel de seguridad del api.
                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api_token);

                //Reemplazamos el numero de identificacion.
                apiUtil._url = apiUtil._url.Replace("{userName}", userName).Replace("{notifique}", notifique.ToString());

                // Envia peticion DELETE
                ResponseDTO response = await apiUtil.SendRequestGetAsync();

                switch (response.StatusCode)
                {
                    /*  Solo en este escenario el Content del response del api es el codigo OTP, 
                        en los otros StatusCode el Content es la descripción el error.  */
                    case 200:
                        return new Login_GetOTP_ResponseDTO()
                        {
                            Message = "Ok",
                            OTP = response.Content,
                            StatusCode = response.StatusCode
                        };
                    case 400:
                        return new Login_GetOTP_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                    case 401:
                        return new Login_GetOTP_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                    case 404:
                        return new Login_GetOTP_ResponseDTO()
                        {
                            Message = "Usuario no encontrado.",
                            StatusCode = response.StatusCode
                        };
                    default:

                        // Al ser un StatusCode distinto a los establecidos, se guarda en el log lo que retorna el api.
                        Exception ex = new Exception($"{response.StatusCode}, {response.Content}");
                        new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_GetOTP> ", exception: ex);

                        return new Login_GetOTP_ResponseDTO()
                        {
                            Message = response.Content,
                            StatusCode = response.StatusCode
                        };
                };
            }
            catch (Exception ex)
            {
                new LogUtil().GuardarLog(modulo: "Utilidades > LoginApi > LoginApi_GetOTP> ", exception: ex);

                return new Login_GetOTP_ResponseDTO()
                {
                    StatusCode = 500,
                    Message = "Error interno"
                };
            }
        }

    }
}
