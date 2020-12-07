namespace Yulius.Helper.Api
{
    public class StringListasUtil
    {

        /// <summary>
        /// Convierte un string separado por , en un string[]
        /// </summary>
        /// <param name="listado">Listado a convertir</param>
        /// <returns>string[]</returns>
        public string[] ConvertirStringEnLista(string listado)
        {
            if (!string.IsNullOrEmpty(listado))
            {
                // Elimino espacion y Separo los items con split
                string[] lista = listado.Replace(" ", "").Split(',');

                string[] resultado = new string[lista.Length];

                if (lista.Length > 0)
                {

                    for (int i = 0; i < lista.Length; i++)
                    {
                        resultado[i] = lista[i];
                    }

                    return resultado;
                }

                return null;
            }

            return null;

        }

    }
}
