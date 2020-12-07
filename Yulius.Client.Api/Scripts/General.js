var Contador = 0;
var RamaSeleccionada = null;

function cambioClave(texto, urlCambio) {

    $.ajax({
        url: urlCambio + "?clave=" + texto,
        type: 'POST',
        cache: false,
        dataType: 'json',
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            if (data == 'true') {
                $('#loginModal').modal('hide');
                bootbox.alert("Clave cambiada correctamente.");
                setTimeout(function () {
                    document.location.reload(true);
                }, 5000);
            }
            else {
                $('#loginModal').modal('hide');
                bootbox.alert("Error al intentar cambiar la Clave.");
                setTimeout(function () {
                    document.location.reload(true);
                }, 5000);
            }

        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Handle errors here
            console.log('ERRORS: ' + textStatus);
            // STOP LOADING SPINNER
        }
    });

}

function FormatearMoneda(Numero, Decimales) {
    var Resultado;
    var decimalplaces = Decimales;
    var decimalcharacter = ".";
    var thousandseparater = ",";
    number = parseFloat(Numero);
    var sign = number < 0 ? "-" : "";
    var formatted = new String(number.toFixed(decimalplaces));
    if (decimalcharacter.length && decimalcharacter != ".") { formatted = formatted.replace(/\./, decimalcharacter); }
    var integer = "";
    var fraction = "";
    var strnumber = new String(formatted);
    var dotpos = decimalcharacter.length ? strnumber.indexOf(decimalcharacter) : -1;
    if (dotpos > -1) {
        if (dotpos) { integer = strnumber.substr(0, dotpos); }
        fraction = strnumber.substr(dotpos + 1);
    }
    else { integer = strnumber; }
    if (integer) { integer = String(Math.abs(integer)); }
    while (fraction.length < decimalplaces) { fraction += "0"; }
    temparray = new Array();
    while (integer.length > 3) {
        temparray.unshift(integer.substr(-3));
        integer = integer.substr(0, integer.length - 3);
    }
    temparray.unshift(integer);
    integer = temparray.join(thousandseparater);

    if (Decimales < 0) {
        Resultado = "$" + sign + integer + decimalcharacter + fraction;
    }
    else {
        Resultado = "$" + sign + integer;
    }

    return Resultado
}

function Enviar() {
    $("#frmPopup").contents().find('form').submit();
}

function AgregarPopUpSimple(Control, Ancho) {
    if (Ancho == null) {
        Ancho = '70%';
    }

    var HtmlPopUp = '<div class="modal fade" id="popup" role="dialog" style="z-index: 10000;" aria-hidden="true"> \
                        <div class="modal-dialog" id="popupInterno" style="width:' + Ancho + ';"> \
                            <div class="modal-content"> \
                            <div class="modal-body" id="popupAlto" style="height:450px"> \
                                <iframe id="frmPopup" name="frmPopup" seamless="seamless" onload="$(\'#Rueda\').hide();" style="width:100%;height:100%;border:none"></iframe> \
                            </div> \
                        </div> \
                    </div> \
                </div>';

    $('#' + Control).append(HtmlPopUp)
}

function GenerarTokenAleatorio() {
    return Math.random().toString(36).substr(2);
};

function AgregarPopUp(Control, Ancho, CallBack, EstiloAdicional, NombrePopup) {
    if (CallBack == null) {
        CallBack = '';
    }
    else {
        CallBack = CallBack + ';';
    }

    if (Ancho == null) {
        Ancho = '70%';
    }

    var NombreInterno = 'popup';
    if (NombrePopup != null) {
        NombreInterno = NombrePopup;
    }

    var HtmlPopUp = '<div class="modal fade" id="' + NombreInterno + '" role="dialog" style="z-index: 99999999;" aria-hidden="true"> \
                        <div class="modal-dialog ' + EstiloAdicional + '" id="popupInterno" style="width:' + Ancho + ';"> \
                            <div class="modal-content"> \
                                <div class="modal-header" style="height:50px"> \
                                    <table style="width:100%"> \
                                        <tr> \
                                            <td> \
                                                <h4 class="modal-title" id="titulopopup"></h4> \
                                            </td> \
                                            <td align="right"> \
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button> \
                                            </td> \
                                        </tr> \
                                    </table> \
                                </div> \
                            <div class="modal-body" id="popupAlto" style="height:450px"> \
                                <iframe id="frmPopup" name="frmPopup" seamless="seamless" onload="$(\'#Rueda\').hide();" style="width:100%;height:100%;border:none"></iframe> \
                            </div> \
                            <div class="modal-footer"> \
                                <table style="width:100%"> \
                                <tr> \
                                    <td style="width:60%" align="left"> \
                                        <img id="Rueda" height="20px" src="' + RutaSitio + '/Content/Images/cargandotexto.gif" /> \
                                    </td> \
                                    <td style="width:15%" align="right"> \
                                        <button type="button" id="popupboton" onclick="Enviar();$(\'#Rueda.show();\')" style="display:none" class="btn btn-sm btn-success">Crear</button> \
                                    </td> \
                                    <td style="width:15%" align="right"> \
                                        <button type="button" id="popupcancelar" class="btn btn-success btn-sm" onclick="$(\'#frmPopup\').attr(\'src\',\'about:blank\');' + CallBack + '" data-dismiss="modal">Cerrar</button> \
                                    </td> \
                                </tr> \
                                </table>   \
                            </div> \
                        </div> \
                    </div> \
                </div>';

    //onclick="$(\'#Rueda\').show(); Enviar();" 
    //<button type="button" id="popupboton" onclick="$(\'#Rueda\').show(); Enviar();" style="display:none" class="btn btn-metrodarktexto">Crear</button> \

    $('#' + Control).append(HtmlPopUp)

    //$('#popupboton').click(function () {
    //    $('#Rueda').show();
    //    Enviar();
    //});
}

function AbrirPopUp(Titulo, Url, MostrarGuardar, Alto, TextoBoton, MostrarCancelar, CallBackGuardar) {
    $('#frmPopup').attr('src', RutaSitio + '/blank.html');
    $('#titulopopup').text(Titulo);
    $('#popup').modal({ backdrop: 'static' }, 'show');
    $('#popupAlto').attr('style', 'height:' + Alto + 'px')
    $('#frmPopup').attr('src', Url);
    $('#Rueda').show();

    if (typeof MostrarGuardar != 'undefined') {
        if (MostrarGuardar) {
            if (TextoBoton != 'undefined') {
                $('#popupboton').text(TextoBoton);
            }
            else {
                $('#popupboton').text('Crear');
            }

            $('#popupboton').show();

            if (typeof CallBackGuardar != 'undefined') {
                try {
                    $("#popupboton").unbind('click');
                    $("#popupboton").off('click');
                    $('#popupboton').click(CallBackGuardar);
                }
                catch (err) {
                    bootbox.alert(err);
                }
            }
        }
        else {
            $('#popupboton').hide();
        }
    }
    else {
        $('#popupboton').hide();
    }

    if (typeof MostrarCancelar != 'undefined') {
        if (MostrarCancelar) {
            $('#popupcancelar').show();
        }
        else {
            $('#popupcancelar').hide();
        }
    }
    else {
        $('#popupcancelar').show();
    }

    try {
        CerrarMenu();
    }
    catch (err)
    { }
}

function AbrirPopUp2(Control, Titulo, Url, MostrarGuardar, Alto, TextoBoton, MostrarCancelar, CallBackGuardar, NombrePopup) {
    var NombreInterno = 'popup';
    if (NombrePopup != null) {
        NombreInterno = NombrePopup;
    }
    $('#' + Control).find('#frmPopup').attr('id', 'frm' + Control);
    $('#' + Control).find('#frm' + Control).attr('src', RutaSitio + '/blank.html');
    $('#' + Control).find('#titulopopup').text(Titulo);
    $('#' + Control).find('#' + NombrePopup).modal({ backdrop: 'static' }, 'show');
    $('#' + Control).find('#popupAlto').attr('style', 'height:' + Alto + 'px')
    $('#' + Control).find('#frm' + Control).attr('src', Url);
    $('#' + Control).find('#Rueda').show();

    if (typeof MostrarGuardar != 'undefined') {
        if (MostrarGuardar) {
            if (TextoBoton != 'undefined') {
                $('#' + Control).find('#popupboton').text(TextoBoton);
            }
            else {
                $('#' + Control).find('#popupboton').text('Crear');
            }

            $('#' + Control).find('#popupboton').show();

            if (typeof CallBackGuardar != 'undefined') {
                try {
                    $('#' + Control).find("#popupboton").unbind('click');
                    $('#' + Control).find("#popupboton").off('click');
                    $('#' + Control).find('#popupboton').click(CallBackGuardar);
                }
                catch (err) {
                    bootbox.alert(err);
                }
            }
        }
        else {
            $('#' + Control).find('#popupboton').hide();
        }
    }
    else {
        $('#' + Control).find('#popupboton').hide();
    }

    if (typeof MostrarCancelar != 'undefined') {
        if (MostrarCancelar) {
            $('#' + Control).find('#popupcancelar').show();
        }
        else {
            $('#' + Control).find('#popupcancelar').hide();
        }
    }
    else {
        $('#' + Control).find('#popupcancelar').show();
    }

    try {
        CerrarMenu();
    }
    catch (err)
    { }
}

function AbrirPopUpAvanzado(Control, Titulo, Url, MostrarGuardar, Alto, TextoBoton, MostrarCancelar) {
    $('#' + Control).find('#frmPopup').attr('src', RutaSitio + '/blank.html');
    $('#' + Control).find('#titulopopup').text(Titulo);
    $('#' + Control).find('#popup').modal({ backdrop: 'static' }, 'show');
    $('#' + Control).find('#popupAlto').attr('style', 'height:' + Alto + 'px')
    $('#' + Control).find('#frmPopup').attr('src', Url);
    $('#' + Control).find('#Rueda').show();

    if (typeof MostrarGuardar != 'undefined') {
        if (MostrarGuardar) {
            if (TextoBoton != 'undefined') {
                $('#' + Control).find('#popupboton').text(TextoBoton);
            }
            else {
                $('#' + Control).find('#popupboton').text('Crear');
            }

            $('#' + Control).find('#popupboton').show();
        }
        else {
            $('#' + Control).find('#popupboton').hide();
        }
    }
    else {
        $('#' + Control).find('#popupboton').hide();
    }

    if (typeof MostrarCancelar != 'undefined') {
        if (MostrarCancelar) {
            $('#' + Control).find('#popupcancelar').show();
        }
        else {
            $('#' + Control).find('#popupcancelar').hide();
        }
    }
    else {
        $('#' + Control).find('#popupcancelar').show();
    }

    try {
        CerrarMenu();
    }
    catch (err)
    { }
}

function MostrarAlerta(Control, Titulo, Texto, MostrarCancelar, GenerarEventoAceptar) {
    var BotonCancelar = '';
    var EventoAceptar = '';
    if (typeof MostrarCancelar != 'undefined') {
        if (MostrarCancelar == true) {
            BotonCancelar = '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<button id="btnCancelarAlerta" type="button" class="btn btn-metrodarktexto btn-xs" data-dismiss="modal">Cancelar</button>';
        }
    }

    if (typeof GenerarEventoAceptar != 'undefined') {
        if (GenerarEventoAceptar == true) {
            EventoAceptar = 'onclick="AlAceptarAlerta();"';
        }
    }

    var HtmlPopUp = '<div  class="modal fade in" id="popupAlerta' + Control + '" role="dialog" style="z-index: 99999999;background-color:transparent;" aria-hidden="true"> \
                    <div class="modal-dialog" style="width:60%;"> \
                        <div class="modal-content"> \
                            <div class="modal-header" style="height:50px"> \
                                    <table style="width:100%"> \
                                        <tr> \
                                            <td> \
                                                <h4 class="modal-title">' + Titulo + '</h4> \
                                            </td> \
                                            <td align="right"> \
                                                <i class="glyphicon glyphicon-remove-circle" id="btnIconoAlerta" style="cursor:pointer" data-dismiss="modal"></i> \
                                            </td> \
                                        </tr> \
                                    </table> \
                            </div> \
                        <div id="popupMensaje" class="modal-body">' + Texto + '</div> \
                        <div style="border-top: 1px solid #e5e5e5;padding: 15px;" align="right"> \
                            <button type="button" data-dismiss="modal" id="btnAceptarAlerta" class="btn btn-metrodarktexto btn-xs"' + EventoAceptar + '>Aceptar</button>' + BotonCancelar + '\
                        </div> \
                    </div> \
                </div> \
            </div>';

    $('#' + Control).append(HtmlPopUp)
    $('#popupAlerta' + Control).modal({ backdrop: 'static' }, 'show');

    $('#popupAlerta' + Control).on('hidden.bs.modal', function (e) {
        $('#' + Control).html('');
    })

}

function AgregarAlertaWindows8(Control, MostrarAceptar, TextoAceptar, MostrarCancelar, TextoCancelar, FuncionAceptar) {
    var BotonAceptar = '';
    var BotonCancelar = ''
    var ControlCompleto = Control + '_Popup';

    if (MostrarAceptar == true) {
        BotonAceptar = '<a class="btn btn-success m-right-sm ' + ControlCompleto + '_close" onClick="' + FuncionAceptar + '">' + TextoAceptar + '</a>';
    }

    if (MostrarCancelar == false) {
        BotonCancelar = 'style="display:none" ';
    }
    var Html = '<div class="custom-popup light width-100" id="' + ControlCompleto + '"> \
                <div class="padding-md"> \
                    <label id="' + Control + '_Mensaje" style="font-size:large" class="m-top-none"></label> \
                </div> \
                <div class="text-center"> \
                   ' + BotonAceptar + ' \
                    <a class="btn btn-danger" ' + BotonCancelar + '>' + TextoCancelar + '</a> \
                </div> \
            </div>';

    $('#' + Control).append(Html);
}

function MostrarAlertaWindows8(Control, Texto) {
    $('#' + Control + '_Mensaje').text(Texto);

    $('#' + Control + '_Popup').popup(
        {
            pagecontainer: '.container',
            transition: 'all 0.3s',
            blur: false
        }
    );
}

function MostrarPaises(Control, CodigPaisSeleccionado) {
    var Ruta = '/Utilidades/ObtenerPaises';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = './Utilidades/ObtenerPaises';
    }

    $('#' + Control).empty();

    $.ajax({
        dataType: 'json',
        async: false,
        type: "GET",
        url: Ruta,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            for (var i = 0; i <= result.length - 1; i++) {
                if (result[i].CodPais == CodigPaisSeleccionado) {
                    $('#' + Control).append('<option selected value=' + result[i].CodPais + '>' + result[i].NombrePais + '</option>')
                }
                else {
                    $('#' + Control).append('<option value=' + result[i].CodPais + '>' + result[i].NombrePais + '</option>')
                }

            }
        }
    });

    if (CodigPaisSeleccionado == -1) {
        $("#" + Control).prop("selectedIndex", -1);
    }
}

function MostrarDepartamentos(Control, CodigoPais, CodigoDepartamentoSeleccionado) {
    var Ruta = '/Utilidades/ObtenerDepartamentos?CodigoPais=';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = './Utilidades/ObtenerDepartamentos?CodigoPais=';
    }

    $('#' + Control).empty();

    $.ajax({
        dataType: 'json',
        async: false,
        type: "GET",
        url: Ruta + CodigoPais,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            for (var i = 0; i <= result.length - 1; i++) {
                if (result[i].CodDepartamento == CodigoDepartamentoSeleccionado) {
                    $('#' + Control).append('<option selected value=' + result[i].CodDepartamento + '>' + result[i].NombreDepartamento + '</option>')
                }
                else {
                    $('#' + Control).append('<option value=' + result[i].CodDepartamento + '>' + result[i].NombreDepartamento + '</option>')
                }

            }
        }
    });

    if (CodigoDepartamentoSeleccionado == -1) {
        $("#" + Control).prop("selectedIndex", -1);
    }
}

function MostrarCiudades(Control, CodigoPais, CodigoDepartamento, CodigoCiudadSeleccionada) {
    var Ruta = '/Utilidades/ObtenerCiudades?CodigoPais=';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = './Utilidades/ObtenerCiudades?CodigoPais=';
    }

    $('#' + Control).empty();

    $.ajax({
        dataType: 'json',
        async: false,
        type: "GET",
        url: Ruta + CodigoPais + "&CodigoDepartamento=" + CodigoDepartamento,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            for (var i = 0; i <= result.length - 1; i++) {
                if (result[i].CodCiudad == CodigoCiudadSeleccionada) {
                    $('#' + Control).append('<option selected value=' + result[i].CodCiudad + '>' + result[i].NombreCiudad + '</option>')
                }
                else {
                    $('#' + Control).append('<option value=' + result[i].CodCiudad + '>' + result[i].NombreCiudad + '</option>')
                }

            }
        }
    });

    if (CodigoCiudadSeleccionada == -1) {
        $("#" + Control).prop("selectedIndex", -1);
    }
}

function MostrarCiudad(Control, CodigoCiudadSeleccionada, urlCiudad) {

    $('#' + Control).val(' ');
    $.ajax({
        dataType: 'json',
        async: false,
        type: "GET",
        url: urlCiudad + '?CodigoCiudad=' + CodigoCiudadSeleccionada,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $('#' + Control).val(result);
        }
    });
}

function Inicializar(Ruta) {
    var Resultado = '';

    $.ajax({
        dataType: 'json',
        async: false,
        cache: false,
        type: "GET",
        url: Ruta,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            Resultado = result.Ruta;
            localStorage.setItem("Token", result.Token);
            localStorage.setItem("RutaWSSeguridad", result.RutaWSSeguridad);
        }
    });

    return Resultado;
}

function CargarIngresar(Operacion, urlScript) {
    try {

        $.getScript(RutaScript, function () {
            var RutaSplit = RutaScript.split('/');
            var NombreSolo = RutaSplit[RutaSplit.length - 1].replace('.js', '');
            var Ruta = ObtenerRuta(Operacion, NombreSolo);
            $.ajax({
                dataType: 'json',
                async: false,
                type: "GET",
                url: urlScript + "?Ruta=" + Ruta,
                contentType: "application/json; charset=utf-8",
                success: function (result) { }
            });
            document.location.href = Ruta;
        });
    }
    catch (err) {
        bootbox.alert(err);
    }
}

function ActivarFormatoNumerico() {
    var Salario = $("#Salario");
    $(Salario).priceFormat({
        prefix: '$ ',
        centsSeparator: '.',
        thousandsSeparator: ',',
        limit: 12,
        centsLimit: 0
    });

    var Ingresos = $("input[data-tipo~='Ingresos']");
    for (var i = 0; i <= Ingresos.length - 1 ; i++) {
        $(Ingresos[i]).priceFormat({
            prefix: '$ ',
            centsSeparator: '.',
            thousandsSeparator: ',',
            limit: 12,
            centsLimit: 0
        });
    }

    var Egresos = $("input[data-tipo~='Egresos']");
    for (var i = 0; i <= Egresos.length - 1 ; i++) {
        $(Egresos[i]).priceFormat({
            prefix: '$ ',
            centsSeparator: '.',
            thousandsSeparator: ',',
            limit: 12,
            centsLimit: 0
        });
    }

    var ValoresComerciales = $("input[id~='ValorComercial']");
    for (var i = 0; i <= ValoresComerciales.length - 1 ; i++) {
        $(ValoresComerciales[i]).priceFormat({
            prefix: '$ ',
            centsSeparator: '.',
            thousandsSeparator: ',',
            limit: 12,
            centsLimit: 0
        });
    }

    var ValoresHipotecas = $("input[id~='ValorHipoteca']");
    for (var i = 0; i <= ValoresHipotecas.length - 1 ; i++) {
        $(ValoresHipotecas[i]).priceFormat({
            prefix: '$ ',
            centsSeparator: '.',
            thousandsSeparator: ',',
            limit: 12,
            centsLimit: 0
        });
    }

    var SaldosDeudas = $("input[id~='SaldoDeuda']");
    for (var i = 0; i <= SaldosDeudas.length - 1 ; i++) {
        $(SaldosDeudas[i]).priceFormat({
            prefix: '$ ',
            centsSeparator: '.',
            thousandsSeparator: ',',
            limit: 12,
            centsLimit: 0
        });
    }

    var ValoresVehiculos = $("input[id~='ValorVehiculo']");
    for (var i = 0; i <= ValoresVehiculos.length - 1 ; i++) {
        $(ValoresVehiculos[i]).priceFormat({
            prefix: '$ ',
            centsSeparator: '.',
            thousandsSeparator: ',',
            limit: 12,
            centsLimit: 0
        });
    }


}

function GuardarImagenServidor(DatosImagen) {
    var Respuesta = '';
    var Ruta = '/Utilidades/GuardarImagen';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = $(location).attr('protocol') + '//' + $(location).attr('host') + $(location).attr('pathname') + "/Utilidades/GuardarImagen"
    }

    Ruta = Ruta.replace('/Simuladores/Credito', '');
    Ruta = Ruta.replace('/Simuladores/CDAT', '');
    Ruta = Ruta.replace('/SolicitudCredito', '');
    Ruta = Ruta.replace('/SolicitudAuxilo', '');
    Ruta = Ruta.replace('/CRM/Eventos', '');
    Ruta = Ruta.replace('/SolicitudAhorro/CrearAhorro', '');
    Ruta = Ruta.replace('/SolicitudAhorro/ModificarAhorro', '');
    Ruta = Ruta.replace('/SolicitudAhorro/CancelacionAhorro', '');

    $.ajax({
        type: "POST",
        url: Ruta,
        async: false,
        data: {
            "InfoImagen": DatosImagen
        },
        success: function (result) {
            Respuesta = result;
        },
        error: function (x, y, z) {
            bootbox.alert(x + '\n' + y + '\n' + z);
        }
    });

    return Respuesta;
}

function GenerarPDF(Html) {
    try {
        $('#Procesando').show();
    }
    catch (err) { }

    var Resultado = '';
    var Ruta = '/Utilidades/GenerarPDF';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = $(location).attr('protocol') + '//' + $(location).attr('host') + $(location).attr('pathname') + "/Utilidades/GenerarPDF"
    }

    Ruta = Ruta.replace('/Simuladores/Credito', '');
    Ruta = Ruta.replace('/Simuladores/CDAT', '');

    $.ajax({
        dataType: 'json',
        async: false,
        type: 'GET',
        url: Ruta + '?Html="' + Html + '"',
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            try {
                Resultado = result;
                $('#Procesando').hide();
            }
            catch (err) { }
        },
        error: function (x, y, z) {
            bootbox.alert(x + '\n' + y + '\n' + z);
        }
    });

    return Resultado;
}

function ActualizarInformacionPerfil() {
    var Ruta = '/Utilidades/ActualizarInformacionPerfil/';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = $(location).attr('protocol') + '//' + $(location).attr('host') + $(location).attr('pathname') + "/Utilidades/ActualizarInformacionPerfil/"
    }

    Ruta = Ruta.replace('/Home/Perfil', '');

    $('#Procesando').show();

    $.ajax({
        url: Ruta + "?Nombre=" + $('#NombrePerfil').val(),
        type: 'POST',
        cache: false,
        dataType: 'json',
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            if (data.indexOf('Error:') == 0) {
                $('#TextoError').text(data);
                $("#MensajeError").fadeTo("slow", 1);
                window.setTimeout(function () {
                    $("#MensajeError").fadeTo("slow", 0);
                }, 5000);
                Resultado = false;
            }
            else {
                $('#TextoExito').text(data);
                $("#MensajeExito").fadeTo("slow", 1);
                window.setTimeout(function () {
                    $("#MensajeExito").fadeTo("slow", 0);
                }, 3000);

                Resultado = true;
            }

            $('#Procesando').hide();

        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Handle errors here
            console.log('ERRORS: ' + textStatus);
            // STOP LOADING SPINNER
        }
    });
}

function CargarFotoPerfil() {
    var Resultado = true;
    var ArchivoTmp = $('input[type="file"]');
    if (ArchivoTmp[0].files.length == 0) {
        return true;
    }

    var InfoTmp = new FormData();
    try {
        var Archivo = ArchivoTmp[0].files[0].name;
        var Extension = Archivo.substr((Archivo.lastIndexOf('.') + 1)).toLowerCase();
        if (Extension == 'jpg' || Extension == 'png') {
            InfoTmp.append(ArchivoTmp[0].files[0].name, ArchivoTmp[0].files[0]);
            ArchivoCarga = ArchivoTmp[0].files[0].name;
        }
        else {
            $('#TextoError').text('El tipo de archivo no es permitido, solo (jpg, png)');
            $("#MensajeError").fadeTo("slow", 1);
            window.setTimeout(function () {
                $("#MensajeError").fadeTo("slow", 0);
            }, 5000);
            return;
        }

    }
    catch (err) {
        bootbox.alert(err);
    }


    var Ruta = '/Utilidades/ImportarImagenPerfil';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion == -1) {
        Ruta = $(location).attr('protocol') + '//' + $(location).attr('host') + $(location).attr('pathname') + "/Utilidades/ImportarImagenPerfil"
    }

    Ruta = Ruta.replace('/Home/Perfil', '');

    $('#Procesando').show();

    $.ajax({
        url: Ruta,
        type: 'POST',
        data: InfoTmp,
        cache: false,
        dataType: 'json',
        processData: false,
        contentType: false,
        async: false,
        success: function (data) {
            if (data.indexOf('Error:') == 0) {
                $('#TextoError').text(data);
                $("#MensajeError").fadeTo("slow", 1);
                window.setTimeout(function () {
                    $("#MensajeError").fadeTo("slow", 0);
                }, 5000);
                Resultado = false;
            }
            else {
                $('#TextoExito').text(data);
                $("#MensajeExito").fadeTo("slow", 1);
                window.setTimeout(function () {
                    $("#MensajeExito").fadeTo("slow", 0);
                }, 3000);

                Resultado = true;
            }

            $('#Procesando').hide();

        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Handle errors here
            console.log('ERRORS: ' + textStatus);
            // STOP LOADING SPINNER
        }
    });

    return Resultado;
}

function EnviarCorreo(CodigoMensajeNotificacion, Mensaje, Destino, Adjunto, Data, urlGuardarCDAT, urlGuardarCredito,urlEnviarCorreo) {
    try {
        $('#Procesando').show();
    }
    catch (err) { }

    var Ruta = urlEnviarCorreo;
    var RutaCDAT = urlGuardarCDAT;
    var RutaCredito = urlGuardarCredito;

    var Info = {
        CodigoMensajeNotificacion: CodigoMensajeNotificacion,
        Mensaje: Mensaje,
        Destino: Destino,
        Adjunto: Adjunto
    };

    Data.Pdf = Adjunto;

    
    
    if (CodigoMensajeNotificacion == '4') {
        //CDAT

        $.ajax({
            dataType: 'json',
            async: false,
            type: 'POST',
            url: RutaCDAT,
            data: JSON.stringify(Data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                bootbox.alert(result);
                try {
                    $.ajax({
                        dataType: 'json',
                        async: false,
                        type: 'POST',
                        url: Ruta,
                        data: JSON.stringify(Info),
                        contentType: "application/json; charset=utf-8",
                        success: function (result) {
                            try {
                                $('#Procesando').hide();
                            }
                            catch (err) { }
                        },
                        error: function (x, y, z) {
                            bootbox.alert(x + '\n' + y + '\n' + z);
                        }
                    });
                }
                catch (err) { }
            },
            error: function (x, y, z) {
                bootbox.alert(x + '\n' + y + '\n' + z);
            }
        });
    }
    else {
        //Credito
       

        $.ajax({
            dataType: 'json',
            async: false,
            type: 'POST',
            url: RutaCredito,
            data: JSON.stringify(Data),
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                bootbox.alert(result);
                try {
                    $.ajax({
                        dataType: 'json',
                        async: false,
                        type: 'POST',
                        url: Ruta,
                        data: JSON.stringify(Info),
                        contentType: "application/json; charset=utf-8",
                        success: function (result) {
                            try {
                                $('#Procesando').hide();
                            }
                            catch (err) { }
                        },
                        error: function (x, y, z) {
                            bootbox.alert(x + '\n' + y + '\n' + z);
                        }
                    });
                }
                catch (err) { }
            },
            error: function (x, y, z) {
                bootbox.alert(x + '\n' + y + '\n' + z);
            }
        });
    }

}

function DescargarDocumento(Control) {
    var Codigo = $(Control).attr('id');
    Codigo = Codigo.replace('Documento_', '');
    var Ruta = '/CargaDocumentos/DescargarDocumento?CodigoDocumento=' + Codigo;

    $('#Descargar').attr('src', Ruta);
}

function GenerarCertificado(CodigoCertificado) {
    //var Ruta = '/Extractos/GenerarCertificado?CodigoCertificado=' + CodigoCertificado;
    var Ruta = 'GenerarCertificado?CodigoCertificado=' + CodigoCertificado;

    $('#Descargar').attr('src', Ruta);
}

$("a[id^='Documento']").click(function () {
    DescargarDocumento(this);
});

$('#Generar').click(function () {
    $('#Procesando').show();
    var CertificadoSeleccionado = $('#Documentos').val();
    GenerarCertificado(CertificadoSeleccionado);
    setTimeout(function () { $('#Procesando').hide(); }, 5000);
});

function ValidarCaracter(Caracter) {
    //var regex = new RegExp("^[a-zA-Z0-9]+$");
    var regex = new RegExp("^[0-9]*$");
    if (regex.test(Caracter)) {
        return true;
    }
    else {
        return false;
    }
};



