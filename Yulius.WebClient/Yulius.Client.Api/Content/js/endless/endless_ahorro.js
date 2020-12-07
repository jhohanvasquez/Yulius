	
// Enviar formulario si acepta terminos

$("input[name='acepto']").change(function () {
var acepto = $("input[name='acepto']:checked").val();
	
	
if(acepto=="1")
{
	$("button[id='enviar']").removeAttr('disabled');
}
else
{
	$("button[id='enviar']").attr('disabled','disabled');
}
});



//  1.Transferencia - 2.Cheque

$("input[name=forma]").change(function () {
var forma = $("input[name=forma]:checked").val();
	
	
if(forma=="1")
{
	$("div[id='banco']").show();
}
else
{
	$("div[id='banco']").hide();
	}
});




