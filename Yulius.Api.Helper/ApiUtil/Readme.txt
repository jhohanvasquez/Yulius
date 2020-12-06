+---------------------------------------------------------------------------------
+ Información General
+---------------------------------------------------------------------------------
Author:         Tatiana Betancur
Fecha :         2019-02-28
Responsable:    Tatiana Betancur
Actualización:  2019-02-28
Versión:        2.0
Descripción:    Utilidad para realizar peticiones a servicios RestFul.
Comentarios:    Se agrega la clase ApiUtilFactory para obtener la instancia
                adecuada de ApiUtil según el tipo de seguridad.
+---------------------------------------------------------------------------------
+ Nuggets
+---------------------------------------------------------------------------------
Newtonsoft.Json		11.0.2

+---------------------------------------------------------------------------------
+ Modo de uso
+---------------------------------------------------------------------------------
Se obtiene la instancia de ApiUtil usando ApiUtilFactory con la información del 
servicio Web Api (ApiDTO), en el que se indica el tipo de seguridad. Luego se hacen las 
peticiones al servicio con la instancia obtenida.

public class Ejemplo
{
    public async void Test()
    {
        ApiDTO api = new ApiDTO();
		api.Identificador = identificador;
		 
		// En caso de tener parametros enviar
		api.parametros = $"{parametro1},{parametro1}, ...;

        ApiUtil apiUtil = ApiUtilFactory.GetApiUtil(api);
		
		ProductoDTO producto = new ProductoDTO();

        // Petición GET
        var response = await apiUtil.SendRequestGetAsync();         

        // Petición POST
        await apiUtil.SendRequestPostAsync(producto);

        // Petición PUT
        await apiUtil.SendRequestPutAsync(productos);

		// Petición DELETE
        var response = await apiUtil.SendRequestDeleteAsync();  
		
		if (response.Item1 == 200) producto = JsonConvert.DeserializeObject<ProductoDTO>(response.Item2);
    }
    public class ProductoDTO { }
}



				ApiDTO api = new ApiDTO();
                api.Identificador = KeyConfig_API.Url_CRM_Obtener_Asociado_Identificacion;

                // En caso de tener parametros enviar
                api.parametros = $"{identificacion}";

                ApiUtil apiUtil = await ApiUtilFactory.GetApiUtil(api);

                // Petición GET
                var response = await apiUtil.SendRequestGetAsync();

                if (response.Item1 == 200)
                {
                    var result = JsonConvert.DeserializeObject<AsociadoWebApiDTO>(response.Item2);

                    if (result.mensajeResult == "Ok")
                    {
                        return result.asociado;
                    }
                }