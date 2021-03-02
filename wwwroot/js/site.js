$(document).ready(function(){
    $('.fa-save').hide();
    $('.check-tel').hide();
    $('.cross-tel').hide();

    $('.fa-pen').on('click', function(){
        var id = $(this).closest('div[class^=persona-body]').find('.id-persona').val();
        $('.save-'+id).show();
        $('.pen-'+id).hide();
        $('.nombre-'+id).removeAttr('disabled');
        $('.fecha-'+id).removeAttr('disabled');
        $('.credito-'+id).removeAttr('disabled');
    });

    $('.fa-save').on('click', function(){
        var id = $(this).closest('div[class^=persona-body]').find('.id-persona').val();
        var nuevo_nombre = $('.nombre-'+id).val();
        var nueva_fecha = $('.fecha-'+id).val();
        var nuevo_credito = $('.credito-'+id).val();

        if(nuevo_nombre === "" || nueva_fecha === "" || nuevo_credito === ""){
            $('.msg-error-'+id).text("Te faltan completar campos.");
        }else{
            $.ajax({
                url: '/Home/DatosEditados',
                type: 'POST',
                data: {
                    ID: id,
                    nombre: nuevo_nombre,
                    fecha: nueva_fecha,
                    credito: nuevo_credito
                }
            });
    
            $('.nombre-'+id).attr('disabled', 'disabled');
            $('.fecha-'+id).attr('disabled', 'disabled');
            $('.credito-'+id).attr('disabled', 'disabled');
            $('.save-'+id).hide();
            $('.pen-'+id).show();
            $('.msg-error-'+id).hide();
        }
    });

    $('.borrar-persona').on('click', function(){
        var id = $(this).closest('div[class^=persona-body]').find('.id-persona').val();
        $('.body-'+id).hide(1000);

        $.ajax({
            url: '/Home/BorrarPersona',
            type: 'POST',
            data: {
                ID: id
            }
        });
    });

    $('.fa-search').on('click', function(){
        var nombre_persona = $('.buscar-personas').val();

        $.ajax({
            url: '/Home/Search?nombre='+nombre_persona,
            method: 'GET',
            success: function(response){
                $('.row').empty();
                for(var i = 0; i < response.length; i++){
                    var fecha = response[i].fechaNacimiento.substring(5, 7)+"/"+response[i].fechaNacimiento.substring(8, 10)+"/"+response[i].fechaNacimiento.substring(0, 4);
                    $('.row').prepend(
                        '<div class="col-md-5 persona-body body-'+response[i].id+'">'+
                            '<input type="hidden" class="id-persona" value="'+response[i].id+'">'+
                            '<p class="msg-error-'+response[i].id+'" style="color: red;"></p>'+
                            '<div class="nombre display-flex margin-bottom">'+
                                '<p class="margin-right">Nombre: </p>'+
                                '<input type="text" value="'+response[i].nombre+'" class="nombre-'+response[i].id+' nombre-input" disabled required>'+
                            '</div>'+
    
                            '<div class="fecha-nacimiento display-flex margin-bottom">'+
                                '<p class="margin-right">Fecha de nacimiento: </p>'+
                                '<input type="text" value="'+fecha+'" class="fecha-'+response[i].id+'" disabled required/>'+
                            '</div>'+
    
                            '<div class="credito display-flex margin-bottom">'+
                                '<p class="margin-right">Crédito máximo: </p>'+
                                '<input type="text" value="'+response[i].creditoMaximo+'" class="credito-'+response[i].id+'" disabled required>'+
                            '</div>'+
    
                            '<div class="options display-flex">'+
                                '<i class="pointer color-white fas fa-pen pen-'+response[i].id+'"></i>'+
                                '<i class="pointer color-white fas fa-save save-'+response[i].id+'"></i>'+
                                '<i class="pointer color-white fas fa-trash borrar-persona"></i>'+
                                '<i class="pointer color-white fas fa-phone"></i>'+
                            '</div>'+
                        '</div>'
                    );
                }
            },
            failure: function(error){
                console.log(error);
            }
        });
        $('.buscar-personas').val("");
    });

    $('.save-telefono').on('click', function(){
        $('.nuevo-telefono').attr('type', 'text');
        $('.check-tel').show();
        $('.cross-tel').show();
    });

    $('.check-tel').on('click', function(){
        var telefono = $('.nuevo-telefono').val();
        var id = $(this).closest('div[class^=telefonos-input-container]').find('.id-persona-telefono').val();

        $.ajax({
            url: '/Home/Nuevo_Telefono',
            type: 'POST',
            data: {
                numero: telefono,
                ID: id
            }
        });

        $('.nuevo-telefono').attr('type', 'hidden');
        $('.check-tel').hide();
        $('.cross-tel').hide();
    });

    $('#agregar-persona').on('click', function(){
        $('#modal-container-close').css('display', 'flex');
    });

    $('.close-modal-container').on('click', function(){
        $('#modal-container-close').css('display', 'none');
    });

    $('.telefonos').on('click', function(){
        $('#modal-telefonos').css('display', 'flex');
        var id = $(this).closest('div[class^=persona-body]').find('.id-persona').val();
        $('.id-persona-telefono').val(id);
        $.ajax({
            url: '/Home/Telefonos_Persona?ID='+id,
            method: 'GET',
            success: function(response){
                $('.telefono-section').empty();
                for(var i = 0; i < response.length; i++){
                    $('.telefono-section').append(
                        '<div class="telefono-agregado content-telefono-'+response[i].telefonoID+'">'+
                            '<input type="text" name="credito" class="input-modal telefono-input telefono-'+response[i].telefonoID+'" value="'+response[i].numero_Telefono+'" required onkeypress="return validaNumericosPrecio(event") disabled>'+
                            '<i class="pointer color-grey margin-left fas fa-redo-alt act-'+response[i].telefonoID+'"></i>'+
                            '<input type="hidden" value="'+response[i].telefonoID+'" class="phone-id">'+
                            '<i class="pointer color-grey margin-left fas fa-pen edit-'+response[i].telefonoID+'"></i>'+
                            '<input type="hidden" value="'+response[i].telefonoID+'" class="phone-id">'+
                            '<i class="pointer color-grey margin-left fas fa-trash borrar-telefono"></i>'+
                            '<input type="hidden" value="'+response[i].telefonoID+'" class="phone-id">'+
                        '</div>'
                    );
                }
                $('.fa-redo-alt').hide();
            },
            failure: function(error){
                console.log(error);
            }
        });
    });

    $('body').on('click', '.fa-pen', function(){
        var id_telefono = $(this).next('.phone-id').val();
        $('.telefono-'+id_telefono).removeAttr('disabled');
        $('.act-'+id_telefono).show();
    });

    $('body').on('click', '.fa-redo-alt', function(){
        var id_telefono = $(this).next('.phone-id').val();
        var nuevo_valor = $('.telefono-'+id_telefono).val();
       
        $.ajax({
            url: '/Home/Actualizar_Numero',
            type: 'POST',
            data: {
                numero: nuevo_valor,
                ID: id_telefono
            }
        });
        $('.telefono-'+id_telefono).attr('disabled', 'disabled');
        $(this).hide('.fa-redo-alt');
    });

    $('body').on('click', '.borrar-telefono', function(){
        var id_telefono = $(this).next('.phone-id').val();
        $('.content-telefono-'+id_telefono).hide(1000);
        
        $.ajax({
            url: '/Home/Borrar_Telefono',
            type: 'POST',
            data: {
                ID: id_telefono
            }
        });
    });

    $('.close-modal-telefonos-container').on('click', function(){
        $('#modal-telefonos').css('display', 'none');
    });
});

function validaNumericosPrecio(event) {
    if(event.charCode >= 48 && event.charCode <= 57 || event.charCode == 44){
        return true;
        }
        return false;        
}

function validaNumericos(event) {
    if(event.charCode >= 48 && event.charCode <= 57){
        return true;
        }
        return false;        
}