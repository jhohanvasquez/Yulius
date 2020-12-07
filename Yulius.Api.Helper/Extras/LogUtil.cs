using System;
using System.IO;
using System.Web;

namespace Yulius.Helper.Api
{
    public class LogUtil
    {



        /// <summary>
        /// Log (Almacena log de excepciones generados por el sistema)
        /// </summary>
        /// <param name="modulo">Nombre módulo que generó la excepción</param>
        /// <param name="exception">Detalle de la excepción</param>
        public void GuardarLog(string modulo, Exception exception)
        {
            DateTime fechaActual = DateTime.Now;

            try
            {
                string _ex = string.Empty;
                if (exception.InnerException == null)
                {
                    _ex = exception.Message;
                }
                else if (exception.InnerException.InnerException == null)
                {
                    _ex = exception.InnerException.Message;
                }
                else if (exception.InnerException.InnerException.InnerException == null)
                {
                    _ex = exception.InnerException.InnerException.Message;
                }
                else if (exception.InnerException.InnerException.InnerException.InnerException == null)
                {
                    _ex = exception.InnerException.InnerException.InnerException.Message;
                }
                else
                {
                    _ex = exception.Message;
                }


                // Carpeta Log
                string _RutaArchivo = HttpContext.Current.Server.MapPath("/Log/");

                // Si no existe la carpeta
                if (!(Directory.Exists(_RutaArchivo)))
                {
                    //Creamos la carpeta
                    Directory.CreateDirectory(_RutaArchivo);
                }

                _RutaArchivo = _RutaArchivo + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                bool _ArchivoExiste = File.Exists(_RutaArchivo);

                // Guardamos Log
                using (StreamWriter oSW = new StreamWriter(_RutaArchivo, _ArchivoExiste))
                {
                    oSW.WriteLine("Módulo: {0}", modulo);
                    oSW.WriteLine("Fecha: {0} - {1}", fechaActual.ToLongDateString(), fechaActual.ToLongTimeString());
                    oSW.WriteLine("Error: {0}", _ex);
                    oSW.WriteLine("------------------------------------------------------------------------");
                    oSW.WriteLine("");
                }
            }
            // Guardar en archivo de texto
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
