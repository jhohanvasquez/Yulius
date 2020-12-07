$(function	()	{

	//Form Wizard 1 (Ahorro -> cancelación)
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
					$('#wizardProgress').css("width","100%");
					
					$('#prevStep1').attr('disabled',false);
					$('#prevStep1').removeClass('disabled');
					
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
			$('#wizardProgress').css("width","50%");
				
			$('#prevStep1').attr('disabled',true);
			$('#prevStep1').addClass('disabled');
			
			$('#nextStep1').attr('disabled',false);
			$('#nextStep1').removeClass('disabled');
			
			$('#wizardProgress').css("width","50%");
		}
		
		return false;
	});
	
	
	
	

});