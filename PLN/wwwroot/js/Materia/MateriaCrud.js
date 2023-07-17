

var URL = 'http://localhost:5108/api/';
//Funcion Callback  que iniciara el GetAll() una vez que la pagina haya cargado por completo
$(document).ready(function () {
    GetAll();
   
})


/*Funcion  que accede al servicio web GetAll, crea elementos TD en html para llenar la tabla
    "Materia que existe en la vista GetAll()"*/
function GetAll() {
    $.ajax({
        type: 'GET',
        url: URL + 'Materia/GetAll',

        success: function (result) {
            $('#Materia tbody').empty();
            $.each(result.objects, function (i, materia) {
                var filas =
                    '<tr>'
                    + '<td class="text-center"> <button class="btn btn-warning" onclick="GetById(' + materia.idMateria + ')">Editar</button></td>'
                    + "<td  id='id' class='text-center' style='display: none;'>" + materia.idMateria + "</td>"
                    + "<td  class='text-center'>" + materia.nombre + "</td>"
                    + "<td class='text-center'>" + materia.costo + "</td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Eliminar(' + materia.idMateria + ')">Eliminar</button></td>'
                    + "</tr>";
                $("#Materia tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.ErrorMessage);
        }
    });
};

function AbrirModal() {
    $("#modalPromociones").modal("show");
    $('#txtIdMateria').val("");
    $('#txtNombre').val("");
    $('#txtCosto').val("");
}

function CerrarModal() {
    $('#modalPromociones').modal('hide');
    $('#txtIdMateria').val("");
    $('#txtNombre').val("");
    $('#txtCosto').val("");
   
}

function Modal() {
    var id = $("#txtIdMateria").val()
    console.log(id)
    var materia = {
        IdMateria: $('#txtIdMateria').val(),
        Nombre: $('#txtNombre').val(),
        Costo: $('#txtCosto').val(),


    }
    if ($("#txtIdMateria").val() == "") {
        materia.IdMateria = 0
        Add(materia);
    }
    else {
        Update(materia);
    }
}
function GetById(idMateria) {
    $.ajax({
        type: 'GET',
        url: URL +'Materia/GetById/' + idMateria,
        success: function (Result) {
            $('#txtIdMateria').val(Result.object.idMateria);
            $('#txtNombre').val(Result.object.nombre);
            $('#txtCosto').val(Result.object.costo);
            
            $('#modalPromociones').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
}

function Eliminar(idMateria) {

    if (confirm("¿Estas seguro de eliminar la materia seleccionadoa?")) {
        $.ajax({
            type: 'DELETE',
            url: URL +'Materia/Delete/' + idMateria,
            success: function (result) {
                $('#modal').modal();
                GetAll();
            },
            error: function (result) {
                alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
            }
        });

    };
}

function Add(materia) {
    
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5108/api/Materia/Add',
        dataType: 'json',
        data: JSON.stringify(materia),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            alert('Se Ingreso Correctamente la materia');
            $('#modal').modal();
            $('#txtIdMateria').val("");
            $('#txtNombre').val("");
            $('#txtCosto').val("");
            GetAll();
            CerrarModal();
        },
        error: function (result) {
            $('#txtIdMateria').val("");
            $('#txtNombre').val("");
            $('#txtCosto').val("");
            alert('Error en la consulta.');

        }
    });
}

function Update(materia) {
    

    $.ajax({
        type: 'POST',
        url: 'http://localhost:5108/api/Materia/Update/' + materia.idMateria,
        dataType: 'json',
        data: JSON.stringify(materia),
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            alert('Se actualizo corretamente la materia');
            $('#modal').modal();
            $('#txtIdMateria').val("");
            $('#txtNombre').val("");
            $('#txtCosto').val("");
            GetAll();
            CerrarModal();
        },
        error: function (result) {
            
            alert('Error en la consulta.');
           
        }
    });
}