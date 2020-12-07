var waitingDialog = waitingDialog || (function ($) {
    'use strict';

	// Creating modal dialog's DOM
	var $dialog = $(
		'<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
		'<div class="modal-dialog modal-m">' +
		'<div class="modal-content">' +
			'<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
			'<div class="modal-body">' +
				'<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar progress-bar-success" style="width: 100%; margin:auto"></div></div>' +
			'</div>' +
		'</div></div></div>');

	return {
		/**
		 * Opens our dialog
		 * @param message Custom message
		 * @param options Custom options:
		 * 				  options.dialogSize - bootstrap postfix for dialog size, e.g. "sm", "m";
		 * 				  options.progressType - bootstrap postfix for progress bar type, e.g. "success", "warning".
		 */
		show: function (message, options) {
			// Assigning defaults
			if (typeof options === 'undefined') {
				options = {};
			}
			if (typeof message === 'undefined') {
				message = 'Loading';
			}
			var settings = $.extend({
				dialogSize: 'm',
				progressType: '',
				onHide: null // This callback runs after the dialog was hidden
			}, options);

			// Configuring dialog
			$dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
			$dialog.find('.progress-bar').attr('class', 'progress-bar');
			if (settings.progressType) {
				$dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
			}
			$dialog.find('h3').text(message);
			// Adding callbacks
			if (typeof settings.onHide === 'function') {
				$dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
					settings.onHide.call($dialog);
				});
			}
			// Opening dialog
			$dialog.modal();
		},
		/**
		 * Closes dialog
		 */
		hide: function () {
			$dialog.modal('hide');
		}
	};

})(jQuery);

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

function GenerarIdEntrada(Control) {
    var Ruta = '/Utilidades/ObtenerIdEntrada';
    var Posicion = $(location).attr('host').toLowerCase().indexOf('localhost');
    if (Posicion === -1) {
        Ruta = '../Utilidades/ObtenerIdEntrada';
    }
    $('#' + Control).empty();

    $.ajax({
        dataType: 'json',
        async: false,
        type: "GET",
        url: Ruta,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            $('#' + Control).val(result);
        }
    });
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
                        <div  id="popupInterno" style="width:' + Ancho + ';"> \
                            <div class="modal-content"> \
                                <div class="modal-header" style="height:50px"> \
                                    <table style="width:100%"> \
                                        <tr> \
                                            <td> \
                                                <h4 class="modal-title" id="titulopopup"></h4> \
                                            </td> \
                                           \
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
                    alert(err);
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

/*Pie*/



/*!
 * jquery.drawPieChart.js
 * Version: 0.3(Beta)
 * Inspired by Chart.js(http://www.chartjs.org/)
 *
 * Copyright 2013 hiro
 * https://github.com/githiro/drawPieChart
 * Released under the MIT license.
 */
; (function ($, undefined) {
    $.fn.drawPieChart = function (data, options) {
        var $this = this,
          W = $this.width(),
          H = $this.height(),
          centerX = W / 2,
          centerY = H / 2,
          cos = Math.cos,
          sin = Math.sin,
          PI = Math.PI,
          settings = $.extend({
              segmentShowStroke: true,
              segmentStrokeColor: "#fff",
              segmentStrokeWidth: 1,
              baseColor: "#fff",
              baseOffset: 15,
              edgeOffset: 30,//offset from edge of $this
              pieSegmentGroupClass: "pieSegmentGroup",
              pieSegmentClass: "pieSegment",
              lightPiesOffset: 12,//lighten pie's width
              lightPiesOpacity: .3,//lighten pie's default opacity
              lightPieClass: "lightPie",
              animation: true,
              animationSteps: 90,
              animationEasing: "easeInOutExpo",
              tipOffsetX: -15,
              tipOffsetY: -45,
              tipClass: "pieTip",
              beforeDraw: function () { },
              afterDrawed: function () { },
              onPieMouseenter: function (e, data) { },
              onPieMouseleave: function (e, data) { },
              onPieClick: function (e, data) { }
          }, options),
          animationOptions = {
              linear: function (t) {
                  return t;
              },
              easeInOutExpo: function (t) {
                  var v = t < .5 ? 8 * t * t * t * t : 1 - 8 * (--t) * t * t * t;
                  return (v > 1) ? 1 : v;
              }
          },
          requestAnimFrame = function () {
              return window.requestAnimationFrame ||
                window.webkitRequestAnimationFrame ||
                window.mozRequestAnimationFrame ||
                window.oRequestAnimationFrame ||
                window.msRequestAnimationFrame ||
                function (callback) {
                    window.setTimeout(callback, 1000 / 60);
                };
          }();

        var $wrapper = $('<svg width="' + W + '" height="' + H + '" viewBox="0 0 ' + W + ' ' + H + '" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"></svg>').appendTo($this);
        var $groups = [],
            $pies = [],
            $lightPies = [],
            easingFunction = animationOptions[settings.animationEasing],
            pieRadius = Min([H / 2, W / 2]) - settings.edgeOffset,
            segmentTotal = 0;

        //Draw base circle
        var drawBasePie = function () {
            var base = document.createElementNS('http://www.w3.org/2000/svg', 'circle');
            var $base = $(base).appendTo($wrapper);
            base.setAttribute("cx", centerX);
            base.setAttribute("cy", centerY);
            base.setAttribute("r", pieRadius + settings.baseOffset);
            base.setAttribute("fill", settings.baseColor);
        }();

        //Set up pie segments wrapper
        var pathGroup = document.createElementNS('http://www.w3.org/2000/svg', 'g');
        var $pathGroup = $(pathGroup).appendTo($wrapper);
        $pathGroup[0].setAttribute("opacity", 0);

        //Set up tooltip
        var $tip = $('<div class="' + settings.tipClass + '" />').appendTo('body').hide(),
          tipW = $tip.width(),
          tipH = $tip.height();

        for (var i = 0, len = data.length; i < len; i++) {
            segmentTotal += data[i].value;
            var g = document.createElementNS('http://www.w3.org/2000/svg', 'g');
            g.setAttribute("data-order", i);
            g.setAttribute("class", settings.pieSegmentGroupClass);
            $groups[i] = $(g).appendTo($pathGroup);
            $groups[i]
              .on("mouseenter", pathMouseEnter)
              .on("mouseleave", pathMouseLeave)
              .on("mousemove", pathMouseMove)
              .on("click", pathClick);

            var p = document.createElementNS('http://www.w3.org/2000/svg', 'path');
            p.setAttribute("stroke-width", settings.segmentStrokeWidth);
            p.setAttribute("stroke", settings.segmentStrokeColor);
            p.setAttribute("stroke-miterlimit", 2);
            p.setAttribute("fill", data[i].color);
            p.setAttribute("class", settings.pieSegmentClass);
            $pies[i] = $(p).appendTo($groups[i]);

            var lp = document.createElementNS('http://www.w3.org/2000/svg', 'path');
            lp.setAttribute("stroke-width", settings.segmentStrokeWidth);
            lp.setAttribute("stroke", settings.segmentStrokeColor);
            lp.setAttribute("stroke-miterlimit", 2);
            lp.setAttribute("fill", data[i].color);
            lp.setAttribute("opacity", settings.lightPiesOpacity);
            lp.setAttribute("class", settings.lightPieClass);
            $lightPies[i] = $(lp).appendTo($groups[i]);
        }

        settings.beforeDraw.call($this);
        //Animation start
        triggerAnimation();

        function pathMouseEnter(e) {
            var index = $(this).data().order;
            $tip.text(data[index].title + ": " + data[index].value).fadeIn(200);
            if ($groups[index][0].getAttribute("data-active") !== "active") {
                $lightPies[index].animate({ opacity: .8 }, 180);
            }
            settings.onPieMouseenter.apply($(this), [e, data]);
        }
        function pathMouseLeave(e) {
            var index = $(this).data().order;
            $tip.hide();
            if ($groups[index][0].getAttribute("data-active") !== "active") {
                $lightPies[index].animate({ opacity: settings.lightPiesOpacity }, 100);
            }
            settings.onPieMouseleave.apply($(this), [e, data]);
        }
        function pathMouseMove(e) {
            $tip.css({
                top: e.pageY + settings.tipOffsetY,
                left: e.pageX - $tip.width() / 2 + settings.tipOffsetX
            });
        }
        function pathClick(e) {
            var index = $(this).data().order;
            var targetGroup = $groups[index][0];
            for (var i = 0, len = data.length; i < len; i++) {
                if (i === index) continue;
                $groups[i][0].setAttribute("data-active", "");
                $lightPies[i].css({ opacity: settings.lightPiesOpacity });
            }
            if (targetGroup.getAttribute("data-active") === "active") {
                targetGroup.setAttribute("data-active", "");
                $lightPies[index].css({ opacity: .8 });
            } else {
                targetGroup.setAttribute("data-active", "active");
                $lightPies[index].css({ opacity: 1 });
            }
            settings.onPieClick.apply($(this), [e, data]);
        }
        function drawPieSegments(animationDecimal) {
            var startRadius = -PI / 2,//-90 degree
                rotateAnimation = 1;
            if (settings.animation) {
                rotateAnimation = animationDecimal;//count up between0~1
            }

            $pathGroup[0].setAttribute("opacity", animationDecimal);

            //draw each path
            for (var i = 0, len = data.length; i < len; i++) {
                var segmentAngle = rotateAnimation * ((data[i].value / segmentTotal) * (PI * 2)),//start radian
                    endRadius = startRadius + segmentAngle,
                    largeArc = ((endRadius - startRadius) % (PI * 2)) > PI ? 1 : 0,
                    startX = centerX + cos(startRadius) * pieRadius,
                    startY = centerY + sin(startRadius) * pieRadius,
                    endX = centerX + cos(endRadius) * pieRadius,
                    endY = centerY + sin(endRadius) * pieRadius,
                    startX2 = centerX + cos(startRadius) * (pieRadius + settings.lightPiesOffset),
                    startY2 = centerY + sin(startRadius) * (pieRadius + settings.lightPiesOffset),
                    endX2 = centerX + cos(endRadius) * (pieRadius + settings.lightPiesOffset),
                    endY2 = centerY + sin(endRadius) * (pieRadius + settings.lightPiesOffset);
                var cmd = [
                  'M', startX, startY,//Move pointer
                  'A', pieRadius, pieRadius, 0, largeArc, 1, endX, endY,//Draw outer arc path
                  'L', centerX, centerY,//Draw line to the center.
                  'Z'//Cloth path
                ];
                var cmd2 = [
                  'M', startX2, startY2,
                  'A', pieRadius + settings.lightPiesOffset, pieRadius + settings.lightPiesOffset, 0, largeArc, 1, endX2, endY2,//Draw outer arc path
                  'L', centerX, centerY,
                  'Z'
                ];
                $pies[i][0].setAttribute("d", cmd.join(' '));
                $lightPies[i][0].setAttribute("d", cmd2.join(' '));
                startRadius += segmentAngle;
            }
        }

        var animFrameAmount = (settings.animation) ? 1 / settings.animationSteps : 1,//if settings.animationSteps is 10, animFrameAmount is 0.1
            animCount = (settings.animation) ? 0 : 1;
        function triggerAnimation() {
            if (settings.animation) {
                requestAnimFrame(animationLoop);
            } else {
                drawPieSegments(1);
            }
        }
        function animationLoop() {
            animCount += animFrameAmount;//animCount start from 0, after "settings.animationSteps"-times executed, animCount reaches 1.
            drawPieSegments(easingFunction(animCount));
            if (animCount < 1) {
                requestAnimFrame(arguments.callee);
            } else {
                settings.afterDrawed.call($this);
            }
        }
        function Max(arr) {
            return Math.max.apply(null, arr);
        }
        function Min(arr) {
            return Math.min.apply(null, arr);
        }
        return $this;
    };
})(jQuery);






