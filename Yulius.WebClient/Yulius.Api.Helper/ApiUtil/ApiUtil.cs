using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    /// <summary>
    /// Métodos predeterminados para consumir servicios Web Api
    /// </summary>
    public class ApiUtil
    {
        private readonly string _uri;
        private readonly string _virtualDirectory;
        private Dictionary<string, string> Headers { get; set; }
        public string _url;

        /// <summary>
        /// Constructor para inicializar el URI y DirectorioVirtual
        /// </summary>        
        /// </remarks><param name="uri">Dominio del servicio.</param>
        /// <param name="virtualDirectory">Si el api está en un directorio virtual del dominio.</param>      
        public ApiUtil(string uri, string virtualDirectory)
        {
            _uri = uri;
            _virtualDirectory = virtualDirectory;
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// Agrega un Header en el encabezado de la petición.
        /// </summary>
        /// <param name="key">Nombre del Header.</param>
        /// <param name="value">Valor del Header.</param>
        public void LoadHeaders(string key, string value) => Headers.Add(key, value);

        /// <summary>
        /// Agrega una lista de Headers en el encabezado de la petición.
        /// </summary>
        /// <param name="headers">Lista de headers con los Key y Value.</param>
        public void LoadHeaders(Dictionary<string, string> headers)
        {
            Headers.DefaultIfEmpty(); // Se limpia la lista de Headers.            
            Headers = headers; // Se agrega la lista de headers.
        }

        /// <summary>
        /// Envia la petición GET a la url, y con la lista de Headers.
        /// </summary>    
        /// <param name="url">Dirección a la que se realiza la petición.</param>
        /// <returns></returns>
        public async Task<ResponseDTO> SendRequestGetAsync()
        {
            // Variable que recibe la respuesta de la petición.
            HttpResponseMessage response = new HttpResponseMessage();

            // Se abre el cliente para realizar peticiones al API
            using (HttpClient client = new HttpClient())
            {
                // Configuraciones de la petición
                client.BaseAddress = new Uri(_uri);
                _url = string.Concat(_virtualDirectory, _url);

                // Se agregar los Headers a la petición
                foreach (KeyValuePair<string, string> header in Headers) client.DefaultRequestHeaders.Add(header.Key, header.Value);

                // Se realiza la petición al servicio
                response = await client.GetAsync(_url);
            }

            // Variable retorno
            ResponseDTO _response = new ResponseDTO()
            {
                Content = await response.Content?.ReadAsStringAsync() ?? string.Empty,
                StatusCode = (int)response.StatusCode
            };

            return _response;
        }

        /// <summary>
        /// Envía un objeto T en el cuerpo de la peticion POST.
        /// </summary>
        /// <typeparam name="T">T es un clase genérica.</typeparam>
        /// <param name="dataModel">Instancia con los datos, serán serializados como un objeto JSON.</param>
        /// <param name="url">Url a la que se realiza la petición.</param>
        /// <returns>Retorna un código StatusCode estandar Http y un String con el contenidos de la respuesta.</returns>
        public async Task<ResponseDTO> SendRequestPostAsync<T>(T dataModel) where T : class
        {
            // Datos para realizar la petición al servicio.
            _url = string.Concat(_virtualDirectory, _url);
            string data = JsonConvert.SerializeObject(dataModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            // Se hace la petición y se guarda la respuesta.
            HttpResponseMessage response = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                // Se agrega la dirección del dominio al cliente.
                client.BaseAddress = new Uri(_uri);
                // Se agregar los Headers a la petición
                foreach (KeyValuePair<string, string> header in Headers) client.DefaultRequestHeaders.Add(header.Key, header.Value);

                // Se hace la petición post, y se guarda la respuesta.
                response = client.PostAsync(_url, content).Result;
            }

            // Variable retorno
            ResponseDTO _response = new ResponseDTO()
            {
                Content = await response.Content?.ReadAsStringAsync() ?? string.Empty,
                StatusCode = (int)response.StatusCode
            };

            return _response;
        }

        /// <summary>
        /// Envía una collección de pares clave/valor en el cuerpo de la peticion POST.
        /// </summary>
        /// <param name="encodedContent">Contenido encode que se envía usando el tipo application/json MIME.</param>
        /// <param name="url">Url a la que se realiza la petición.</param>
        /// <returns>Retorna un código StatusCode estandar Http y un String con el contenidos de la respuesta.</returns>
        public async Task<ResponseDTO> SendRequestPostAsync(List<KeyValuePair<string, string>> encodedContent, string url)
        {
            var json = JsonConvert.SerializeObject(encodedContent); // or JsonSerializer.Serialize if using System.Text.Json
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

            //Se construye la url        
            url = string.Concat(_virtualDirectory, url);

            // Se realiza la petición al servicio, y se cierra la conexión después de obtener la respuesta.
            HttpResponseMessage response = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                //Se hace el request al web api
                client.BaseAddress = new Uri(_uri);
                response = await client.PostAsync(url, stringContent);
            }

            // Variable retorno
            ResponseDTO _response = new ResponseDTO()
            {
                Content = await response.Content?.ReadAsStringAsync() ?? string.Empty,
                StatusCode = (int)response.StatusCode
            };

            return _response;
        }

        /// <summary>
        /// Envía un objeto T en el cuerpo de la peticion PUT.
        /// </summary>
        /// <typeparam name="T">T es un clase genérica.</typeparam>
        /// <param name="dataModel">Instancia con los datos, serán serializados como un objeto JSON.</param>
        /// <param name="url">Url a la que se realiza la petición.</param>
        /// <returns>Retorna un código StatusCode estandar Http y un String con el contenidos de la respuesta.</returns>
        public async Task<ResponseDTO> SendRequestPutAsync<T>(T dataModel) where T : class
        {
            // Datos para realizar la petición al servicio.
            _url = string.Concat(_virtualDirectory, _url);
            string data = JsonConvert.SerializeObject(dataModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            // Se hace la petición y se guarda la respuesta.
            HttpResponseMessage response = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                // Se agrega la dirección del dominio al cliente.
                client.BaseAddress = new Uri(_uri);

                // Se agregar los Headers a la petición
                foreach (KeyValuePair<string, string> header in Headers) client.DefaultRequestHeaders.Add(header.Key, header.Value);

                // Se hace la petición post, y se guarda la respuesta.
                response = await client.PutAsync(_url, content);
            }

            // Variable retorno
            ResponseDTO _response = new ResponseDTO()
            {
                Content = await response.Content?.ReadAsStringAsync() ?? string.Empty,
                StatusCode = (int)response.StatusCode
            };

            return _response;
        }

        /// <summary>
        /// Envia la petición DELETE a la url, y con la lista de Headers.
        /// </summary>    
        /// <param name="url">Dirección a la que se realiza la petición.</param>
        /// <returns></returns>
        public async Task<ResponseDTO> SendRequestDeleteAsync()
        {
            // Variable de consumo
            HttpResponseMessage response = new HttpResponseMessage();

            // Se abre el cliente para realizar peticiones al API
            using (HttpClient client = new HttpClient())
            {
                // Configuraciones de la petición
                client.BaseAddress = new Uri(_uri);
                _url = string.Concat(_virtualDirectory, _url);

                // Se agregar los Headers a la petición
                foreach (KeyValuePair<string, string> header in Headers) client.DefaultRequestHeaders.Add(header.Key, header.Value);

                // Se realiza la petición al servicio
                response = await client.DeleteAsync(_url);
            }

            // Variable retorno
            ResponseDTO _response = new ResponseDTO()
            {
                Content = await response.Content?.ReadAsStringAsync() ?? string.Empty,
                StatusCode = (int)response.StatusCode
            };

            return _response;
        }
    }
}