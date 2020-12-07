// JavaScript Document

	function pasuser(form) {
	if (form.usuario.value=="ajovel")
	 { 
		if (form.password.value=="71792912")
		{              
		location="index.html" 
		//document.cookie = "user=ajovel";
		}
		else {
		alert("Clave no Válida")
		location="login.html" 
		}
	} 
	else 
	{  
	alert("Usuario no Válido")
	location="login.html" 
	}
	}


