$(document).ready(function(){
	
//  1.si contactar - 2.No contactar

$("input[name=contactar]").change(function () {
var contactar = $("input[name=contactar]:checked").val();
	
if(contactar=="1")
{
	$("div[id='contacto']").show();
}
else
{
	$("div[id='contacto']").hide();
	}
});
});


$(function	()	{

	//Form Wizard 1 (Crédito)
	var currentStep_1 = 1;
	
	$('.wizard-demo li a').click(function()	{
		return true;
	});
	 	 
	$('#formWizard1').parsley( { listeners: {
		onFieldValidate: function ( elem ) {
			// if field is not visible, do not apply Parsley validation!
			if ( !$( elem ).is( ':visible' ) ) {
				return true;
			}			

			return false;
		},
        onFormSubmit: function ( isFormValid, event ) {
            if(isFormValid)	{
					
				currentStep_1++;
				
				if(currentStep_1 == 2)	{
					$('#wizardDemo1 li:eq(1) a').tab('show');
					$('#wizardProgress').css("width","66%");
					
					$('#prevStep1').attr('disabled',false);
					$('#prevStep1').removeClass('disabled');
				}

				else if(currentStep_1 == 3)	{
					$('#wizardDemo1 li:eq(2) a').tab('show');
					$('#wizardProgress').css("width","100%");
					
					$('#nextStep1').attr('disabled',true);
					$('#nextStep1').addClass('disabled');
				}
				
				return false;
			}
        }
    }});
		
	$('#prevStep1').click(function()	{
		
		currentStep_1--;
		
		if(currentStep_1 == 1)	{
		
			$('#wizardDemo1 li:eq(0) a').tab('show');
			$('#wizardProgress').css("width","66%");
				
			$('#prevStep1').attr('disabled',true);
			$('#prevStep1').addClass('disabled');
			
			$('#wizardProgress').css("width","33%");
		}
		else if(currentStep_1 == 2)	{
		
			$('#wizardDemo1 li:eq(1) a').tab('show');
			$('#wizardProgress').css("width","66%");
					
			$('#nextStep1').attr('disabled',false);
			$('#nextStep1').removeClass('disabled');
			
			$('#wizardProgress').css("width","66%");
		}
		
		return false;
	});
	
			

});