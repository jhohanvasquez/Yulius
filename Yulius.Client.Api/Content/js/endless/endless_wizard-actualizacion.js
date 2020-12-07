// dependientes de Cargo
//<option value="1">Pensionado</option>
//<option value="2">Cesante</option>
//<option value="3">Hogar</option>

$("select[name=sOcupacion]").change(function () {
var cargo = $("#sOcupacion option:selected").val();
if (cargo > 3) {
    $(".depOcup").show();
}
else {
    $(".depOcup").hide();
}
});


// Adicionar nueva fila para Bienes raices

$("#btnAddBR").on("click", function () {

    var $tableBodyBR = $('#tblBR').find("tbody"),
            $trLastBR = $tableBodyBR.find("tr:last"),
            $trNewBR = $trLastBR.clone();
            $trLastBR.after($trNewBR);
});

$("#btnAddV").on("click", function () {

    var $tableBodyV = $('#tblV').find("tbody"),
            $trLastV = $tableBodyV.find("tr:last"),
            $trNewV = $trLastV.clone();
            $trLastV.after($trNewV);
});

$("#btnAddPaC").on("click", function () {

    var $tableBodyV = $('#tblPaC').find("tbody"),
            $trLastV = $tableBodyV.find("tr:last"),
            $trNewV = $trLastV.clone();
            $trLastV.after($trNewV);
});


$(function () {

	//Form Wizard 1 (Actualización de datos)
	var currentStep_1 = 1;
	
	$('.wizard-demo li a').click(function () {
	    return true;
	});

	$('#formWizard1').parsley({
	    listeners: {
	        onFieldValidate: function (elem) {
	            // if field is not visible, do not apply Parsley validation!
	            if (!$(elem).is(':visible')) {
	                return true;
	            }
	            return false;
	        },

	        onFormSubmit: function (isFormValid, event) {
	            if (isFormValid) {
	                currentStep_1++;
	                if (currentStep_1 == 2) {

	                    $('#wizardDemo1 li:eq(1) a').tab('show');
	                    $('#wizardProgress').css("width", "28%");

	                    $('#prevStep1').attr('disabled', false);
	                    $('#prevStep1').removeClass('disabled');
	                }

	                else if (currentStep_1 == 3) {

	                    $('#wizardDemo1 li:eq(2) a').tab('show');
	                    $('#wizardProgress').css("width", "42%");
	                }

	                else if (currentStep_1 == 4) {

	                    $('#wizardDemo1 li:eq(3) a').tab('show');
	                    $('#wizardProgress').css("width", "56%");
	                }

	                else if (currentStep_1 == 5) {

	                    $('#wizardDemo1 li:eq(4) a').tab('show');
	                    $('#wizardProgress').css("width", "70%");
	                }

	                else if (currentStep_1 == 6) {

	                    $('#wizardDemo1 li:eq(5) a').tab('show');
	                    $('#wizardProgress').css("width", "84%");
	                }

	                else if (currentStep_1 == 7) {

	                    $('#wizardDemo1 li:eq(6) a').tab('show');
	                    $('#wizardProgress').css("width", "100%");

	                    $('#nextStep1').attr('disabled', true);
	                    $('#nextStep1').addClass('disabled');
	                }

	                return false;
	            }
	        }
	    }
	});

	$('#prevStep1').click(function () {
	    currentStep_1--;
	    if (currentStep_1 == 1) {

	        $('#wizardDemo1 li:eq(0) a').tab('show');
	        $('#wizardProgress').css("width", "14%");

	        $('#prevStep1').attr('disabled', true);
	        $('#prevStep1').addClass('disabled');
	    }
	    else if (currentStep_1 == 2) {

	        $('#wizardDemo1 li:eq(1) a').tab('show');
	        $('#wizardProgress').css("width", "28%");
	        $('#nextStep1').attr('disabled', false);
	        $('#nextStep1').removeClass('disabled');
	    }

	    else if (currentStep_1 == 3) {

	        $('#wizardDemo1 li:eq(2) a').tab('show');
	        $('#wizardProgress').css("width", "42%");
	        $('#nextStep1').attr('disabled', false);
	        $('#nextStep1').removeClass('disabled');
	    }

	    else if (currentStep_1 == 4) {

	        $('#wizardDemo1 li:eq(3) a').tab('show');
	        $('#wizardProgress').css("width", "56%");
	        $('#nextStep1').attr('disabled', false);
	        $('#nextStep1').removeClass('disabled');
	    }

	    else if (currentStep_1 == 5) {

	        $('#wizardDemo1 li:eq(4) a').tab('show');
	        $('#wizardProgress').css("width", "70%");
	        $('#nextStep1').attr('disabled', false);
	        $('#nextStep1').removeClass('disabled');
	    }

	    else if (currentStep_1 == 6) {

	        $('#wizardDemo1 li:eq(5) a').tab('show');
	        $('#wizardProgress').css("width", "84%");
	        $('#nextStep1').attr('disabled', false);
	        $('#nextStep1').removeClass('disabled');
	    }

	    return false;
	});


});