var agenda = {}, listEvento = new Array();
$(document).ready(function () {
    "use strict";
    getDoctores();
    getPasientes();
    $("#fechainicio").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#horainicio").datetimepicker({ format: 'LT' });
    $("#horafin").datetimepicker({ format: 'LT' });
    
    var hdr = {
        left: 'title',
        center: 'month,agendaWeek,agendaDay',
        right: 'prev,today,next'
    };
       
    /* initialize the calendar
     -----------------------------------------------------------------*/

    $('#calendar').fullCalendar({
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
        header: hdr,
        buttonText: {
            prev: '<i class="fa fa-chevron-left"></i>',
            next: '<i class="fa fa-chevron-right"></i>',
            today: 'hoy',
            month: 'mes',
            week: 'semana',
            day: 'día'
        },

        editable: true,
        droppable: false, // this allows things to be dropped onto the calendar !!!
        eventLimit: true,
        disableResizing: true,
        events: function (start, end, callback) {
            start = $.fullCalendar.formatDate(start, "yyyy-MM-dd HH:mm:ss");
            end = $.fullCalendar.formatDate(end, "yyyy-MM-dd HH:mm:ss");
            $.ajax({
                url: '/api/quotes',
                type: 'GET',
                dataType: "json",
                data: { start: start, end: end },
                error: function (xhr, type, exception) { alert("Error: " + exception); },
                success: function (doc) {
                    var events = [];
                    listEvento = new Array();
                    listEvento = doc;
                    $.each(doc, function (i, el) {
                        var ss = {};
                        ss.id = el.id;
                        ss.title = el.titulo;
                        ss.description = 'Doctor: '+el.name_doctor + '<br>Paciente: ' + el.name_paciente;
                        if (el.fechainicio != null) {
                            ss.start = formatDate(el.fechainicio);
                        }
                        if (el.fechainicio != el.fechafin) {
                            if (el.fechafin != null && el.fechafin != "") {
                                ss.end = formatDate(el.fechafin);
                            }
                        }
                        ss.allDay = false;
                        if (el.txtcolor != '')
                            ss.className = ["event", el.txtcolor];
                        if (el.txticono != '')
                            ss.icon = el.txticono;
                        events.push(ss);
                    });
                    if (typeof (callback) === 'function') {
                        callback(events);
                    }
                }
            });
        },
        eventClick: function (event, jsEvent, view) {
            irUrl(event.id);
        },
        eventDrop: function (event, delta, revertFunc) {
            event.end = $.fullCalendar.formatDate(event.end, "yyyy-MM-dd HH:mm:ss");
            event.start = $.fullCalendar.formatDate(event.start, "yyyy-MM-dd HH:mm:ss");
            //UpdateAgenda(event);           
            //GetVencidosPendientes();
            $('#calendar').fullCalendar('refetchEvents');
        },
        eventRender: function (event, element, icon) {
            if (!event.description == "") {
                element.find('.fc-event-title').append("<br/><span class='ultra-light'>" + event.description +
                    "</span>");
            }
            if (!event.icon == "") {
                element.find('.fc-event-title').append("<i class='air air-top-right fa " + event.icon +
                    " '></i>");
            }
        },

        windowResize: function (event, ui) {
            $('#calendar').fullCalendar('render');
        }
    });
    $('.fc-header-right, .fc-header-center').hide();


    $('#calendar-buttons #btn-prev').click(function () {
        $('.fc-button-prev').click();
        return false;
    });

    $('#calendar-buttons #btn-next').click(function () {
        $('.fc-button-next').click();
        return false;
    });

    $('#calendar-buttons #btn-today').click(function () {
        $('.fc-button-today').click();
        return false;
    });

    $('#mt').click(function () {
        $('#calendar').fullCalendar('changeView', 'month');
    });

    $('#ag').click(function () {
        $('#calendar').fullCalendar('changeView', 'agendaWeek');
    });

    $('#td').click(function () {
        $('#calendar').fullCalendar('changeView', 'agendaDay');
    });
    //$('#calendar').fullCalendar('changeView', 'agendaDay');
})

function Save() {
    if (document.getElementById('add-event-form').checkValidity()) {
        var fechaInicio = $('#fechainicio').val(),
               horaInicio = $('#horainicio').val(),
               //fechaFin = $('#fechafin').val(),
               horaFin = $('#horafin').val();
        var cita = {};
        cita.id = $('#hidid').val();
        cita.iddoctor = $('#dlldoctor').val();
        cita.idpaciente = $('#dllpaciente').val();
        cita.titulo = $('#titulo').val();
        cita.descripcion = 'XX';
        cita.fechainicio = fechaInicio+' '+horaInicio;//getFormattedDate(fechaInicio) + ' ' + getHoraDate(horaInicio);
        cita.fechafin = fechaInicio+' '+horaFin;//getFormattedDate(fechaInicio) + ' ' + getHoraDate(horaFin);
        cita.txticono = $('input:radio[name=iconselect]:checked').val();
        cita.txtcolor = $('input:radio[name=priority]:checked').val();
        cita.estado = $('#estado').val();
        $.ajax({
            url: "/api/quotes",
            data: cita,
            type: "POST",
            success: function (data) {
                if (data == true) {
                    alert('La transacción fue un exito');
                    limpiar();
                    $('#calendar').fullCalendar('refetchEvents');
                }
                else {
                    alert('Ya existe para este doctor una cita reservada en el rango de fecha');
                }
            },
            error: function (xhr, type, exception) { alert("Error: " + exception); }
        });
    }
    return false;
}
function limpiar() {
    $("#hidid").val(0);
    $('#dlldoctor').val('');
    $('#dllpaciente').val('');
    $('#titulo').val('');
    //$('#descripcion').val('');
    $('#fechainicio').val('');
    $('#horainicio').val('');
    $('#horafin').val('');
    $('#estado').val(1)
    $("#btneliminar").hide();
}
function getDoctores() {
    GetJsonSync("/api/doctors", {}, function (data) {
        $.each(data, function (i, item) {
            $("#dlldoctor").append('<option value="' + item.id + '">' + item.first_name + ' ' + item.last_name + '</option>');
        });
    })
     .fail(function (jqxhr, textStatus, error) {
         var err = textStatus + ", " + error;
         alert(err);
     });
}
function getPasientes() {
    GetJsonSync("/api/patients", {}, function (data) {
        $.each(data, function (i, item) {
            $("#dllpaciente").append('<option value="' + item.id + '">' + item.first_name + ' ' + item.last_name + '</option>');
        });
    })
     .fail(function (jqxhr, textStatus, error) {
         var err = textStatus + ", " + error;
         alert(err);
     });
}

function getFormattedDate(date) {
    var valor = date.substring(0, 10).split("-");
    return valor[0] + '/' + valor[1] + '/' + valor[2];
}
function getHoraDate(date) {
    var valor = date.split("T");
    if (valor.length == 2)
    {
        var ho = valor[1],
            h = ho.split(':');
        return h[0] + ':' + h[1];
    }
    return "";
}
function UpdateAgenda(evento) {
    $.ajax({
        url: '/Agendas/Agenda/UpdateAgenda',
        type: "post",
        data: evento,
        success: function (data) {
            if (typeof (data) == 'number') {
                $('#calendar').fullCalendar('refetchEvents');
            }
        }
    }).fail(function (jqxhr, textStatus, error) {
        var err = textStatus + ", " + error;
        alert(err);
    });
}

function GetJsonSync(url, data, success) {
    return $.ajax({
        dataType: "json",
        url: url,
        data: data,
        async: false,
        cache: true,
        success: success
    });
}
function formatDate(t)
{
    var data = t.split('T'),
        ho=data[1],
        fe = data[0],
        aa = fe.split('-'),
        h=ho.split(':');
    var d = new Date(parseInt(aa[0]), parseInt(aa[2])-1, parseInt(aa[1]), parseInt(h[0]), parseInt(h[1]), 0, 0);
    return d;
}
function irUrl(id)
{
    var doc = $.grep(listEvento, function (el, i) { return (el.id == id); })[0];
    if (doc != undefined)
    {
        $("#hidid").val(doc.id);
        $('#dlldoctor').val(doc.iddoctor);
        $('#dllpaciente').val(doc.idpaciente);
        $('#titulo').val(doc.titulo);
        //$('#descripcion').val(doc.descripcion);
        $('#fechainicio').val(getFormattedDate(doc.fechainicio));
        $('#horainicio').val(getHoraDate(doc.fechainicio));
        $('#horafin').val(getHoraDate(doc.fechafin));
        $('#estado').val(doc.estado);
        $("#btneliminar").show();
    }
}
function Eliminar()
{
    var id = $("#hidid").val();
    if (id != "" && window.confirm('Esta Seguro de que desea eliminar!!..'))
    {
        //_ajax_request('/api/quotes', { id: id }, function (data) {
        //        if (data == true) {
        //            alert('La transaccion se realizo con exito');
        //            limpiar();
        //            $('#calendar').fullCalendar('refetchEvents');
        //        }
        //    },'', "DELETE"
        //    ).fail(function (jqxhr, textStatus, error) {
        //        var err = textStatus + ", " + error;
        //        alert(err);
        //    });
        $.ajax({
                dataType: "json",
                url: '/api/quotes/Eliminar',
                type: 'GET',
                data: {id: id},
                async: false,
                cache: true,
                success: function (data) {
                if (data == true) {
                    alert('La transaccion se realizo con exito');
                    limpiar();
                    $('#calendar').fullCalendar('refetchEvents');
                }
        }
        }).fail(function (jqxhr, textStatus, error) {
         var err = textStatus + ", " +error;
         alert(err);
        });
    }
}
function _ajax_request(url, data, callback, type, method) {
    if (jQuery.isFunction(data)) {
        callback = data;
        data = {};
    }
    return jQuery.ajax({
        type: method,
        url: url,
        data: data,
        success: callback,
        dataType: type
    });
}
jQuery.extend({
    put: function (url, data, callback, type) {
        return _ajax_request(url, data, callback, type, 'PUT');
    },
    delete_: function (url, data, callback, type) {
        return _ajax_request(url, data, callback, type, 'DELETE');
    }
});