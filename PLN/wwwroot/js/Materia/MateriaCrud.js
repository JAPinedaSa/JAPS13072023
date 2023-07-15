
//Funcion Callback  que iniciara el GetAll() una vez que la pagina haya cargado por completo
$(document).ready(function () {
    GetAll();
   
})

/*Funcion  que accede al servicio web GetAll, crea elementos TD en html para llenar la tabla
    "Materia que existe en la vista GetAll()"*/
function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5108/api/Materia/GetAll',

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
}

function CerrarModal() {
    $('#modalPromociones').modal('hide');
   
}

function Modal() {
    var id = $("#txtIdMateria").val()
    console.log(id)
    var materia = {

        Nombre: $('#txtNombre').val(),
        Costo: $('#txtCosto').val(),

    }
    if ($("#txtIdMateria").val() == "") {
        Add(materia);
    }
    else {
        Update(materia);
    }
}
function GetById(idMateria) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5108/api/Materia/GetById/' + idMateria,
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

function Eliminar(IdEmpleado) {

    if (confirm("¿Estas seguro de eliminar el empleado seleccionado?")) {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:5108/api/Materia/Delete/' + idMateria,
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
        contentType: 'aplication/json; charset=utf-8',
        success: function (result) {
            alert('Se Ingreso Correctamente el empleado');
            $('#modal').modal();
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
}

function Update(idMateria) {
    var materia = {
        
        Nombre: $('#txtNombre').val(),
        Costo: $('#txtCosto').val(),

    }
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5108/api/Materia/Update' + idMateria,
        dataType: 'json',
        data: materia,
        success: function (result) {
            alert('Se actualizo corretamente el empleado');
            $('#modal').modal();
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
}