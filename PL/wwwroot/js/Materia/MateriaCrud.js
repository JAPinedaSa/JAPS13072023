$(document).ready(function () {
    GetAll();
        
})

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5108/api/Materia/GetAll',

        success: function (result) {
            $('#Materia tbody').empty();
            $.each(result.Objects, function (i, materia) {
                var filas =
                    '<tr>'
                    + '<td class="text-center"> <button class="btn btn-warning" onclick="GetById(' + materia.IdMateria + ')"></button></td>'
                    + "<td  id='id' class='text-center' style='display: none;'>" + materia.IdMateria + "</td>"
                    + "<td class='text-center'>" + materia.Nombre + "</td>"
                    + "<td class='text-center'>" + materia.Costo + "</td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Eliminar(' + materia.IdMateria + ')">Eliminar</button></td>'
                    + "</tr>";
                $("#Materia tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.ErrorMessage);
        }
    });
};