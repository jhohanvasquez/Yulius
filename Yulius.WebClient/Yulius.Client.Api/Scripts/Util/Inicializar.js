function Iniciar1() {
    try {
        var Token = localStorage.getItem("Token");
        var RutaWSSeguridad = localStorage.getItem("RutaWSSeguridad");

        $('#Formulario').attr('action', RutaWSSeguridad + 'login.aspx')
        $('#Token').val(Token);

        document.forms[0].submit();
    }
    catch (err) {
        alert(err);
    }
}

function Iniciar2()
{
    try {
        var Token = localStorage.getItem("Token");
        var RutaWSSeguridad = localStorage.getItem("RutaWSSeguridad");

        $('#Formulario').attr('action',RutaWSSeguridad + 'recuperarcontrasena.aspx')
        $('#Token').val(Token);

        document.forms[0].submit();
    }
    catch (err)
    {
        alert(err);
    }
}

function Iniciar3() {
    try {
        var Token = localStorage.getItem("Token");
        var RutaWSSeguridad = localStorage.getItem("RutaWSSeguridad");

        $('#Formulario').attr('action', RutaWSSeguridad + 'reenvioactivacion.aspx')
        $('#Token').val(Token);

        document.forms[0].submit();
    }
    catch (err) {
        alert(err);
    }
}

function Iniciar4() {
    try {
        var Token = localStorage.getItem("Token");
        var RutaWSSeguridad = localStorage.getItem("RutaWSSeguridad");

        $('#Formulario').attr('action', RutaWSSeguridad + 'registrar.aspx')
        $('#Token').val(Token);

        document.forms[0].submit();
    }
    catch (err) {
        alert(err);
    }
}

function Iniciar5() {
    try {
        var Token = localStorage.getItem("Token");
        var RutaWSSeguridad = localStorage.getItem("RutaWSSeguridad");

        $('#Formulario').attr('action', RutaWSSeguridad + 'recuperarusuario.aspx')
        $('#Token').val(Token);

        document.forms[0].submit();
    }
    catch (err) {
        alert(err);
    }
}