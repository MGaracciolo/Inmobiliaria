// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {
    $('#inmueblesTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets":4},
            { "width": "150px", "targets": 4 }
        ],
        "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Inmuebles",
            "infoEmpty": "Mostrando 0 a 0 de 0 Inmuebles",
            "infoFiltered": "(Filtrado de _MAX_ total Inmuebles)",
            "search": "Buscador:",
            "lengthMenu": "Mostrar _MENU_ Inmuebles",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        
        }
    });
    $('#propietariosTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets":5 },
            { "width": "150px", "targets": 5 }
        ],
       "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Propietarios",
            "infoEmpty": "Mostrando 0 a 0 de 0 Propietarios",
            "infoFiltered": "(Filtrado de _MAX_ total Propietarios)",
            "search": "Buscador:",
            "lengthMenu": "Mostrar _MENU_ Propietarios",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
    $('#usuariosTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 0 },
            { "orderable": false, "targets": 5 },
            { "width": "150px", "targets": 5 }
        ],
       "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Usuarios",
            "infoEmpty": "Mostrando 0 a 0 de 0 Usuarios",
            "infoFiltered": "(Filtrado de _MAX_ total Usuarios)",
            "search": "Buscador:",
            "lengthMenu": "Mostrar _MENU_ Usuarios",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
    $('#inquilinosTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 5 },
            { "width": "150px", "targets": 5 }
        ],
       "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Inquilinos",
            "infoEmpty": "Mostrando 0 a 0 de 0 Inquilinos",
            "infoFiltered": "(Filtrado de _MAX_ total Inquilinos)",
            "search": "Buscador:",
            "lengthMenu": "Mostrar _MENU_ Inquilinos",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        
        }
    });
    $('#contratosInmueblesTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 5 }
        ],
      "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Contratos",
            "infoEmpty": "Mostrando 0 a 0 de 0 Contratos",
            "infoFiltered": "(Filtrado de _MAX_ total Contratos)",
            "search": "Buscador:",
            "lengthMenu": "Mostrar _MENU_ Contratos",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
    $('#contratosTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 5 },
            { "width": "180px", "targets": 5 }
        ],
      "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Contratos",
            "infoEmpty": "Mostrando 0 a 0 de 0 Contratos",
            "infoFiltered": "(Filtrado de _MAX_ total Contratos)",
            "search": "Buscador:",
            "lengthMenu": "Mostrar _MENU_ Contratos",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
    $('#pagosTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 5 },
            { "width": "180px", "targets": 5 }
        ],
      "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Pagos",
            "infoEmpty": "Mostrando 0 a 0 de 0 Pagos",
            "infoFiltered": "(Filtrado de _MAX_ total Pagos)",
            "search": "Buscador:",
            "lengthMenu": "Mostrar _MENU_ Pagos",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
    $('#contratosPagosTable').DataTable({
      "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Pagos",
            "infoEmpty": "Mostrando 0 a 0 de 0 Pagos",
            "infoFiltered": "(Filtrado de _MAX_ total Pagos)",
            "search": "Buscador:",
            "lengthMenu": "Mostrar _MENU_ Pagos",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
});
document.addEventListener('DOMContentLoaded', function() {
    const inicio = document.getElementById('inicio');
    const fin = document.getElementById('fin');

   //esto pq todas las fechas de fin sean mayores a la de inicio ndea
    if (inicio && fin) {
   
        inicio.addEventListener('change', function() {
            var inicioValue = inicio.value;

            if (inicioValue) {
                var fechaInicio = new Date(inicioValue);
                fechaInicio.setDate(fechaInicio.getDate() + 1);
                var fechaMin = fechaInicio.toISOString().split('T')[0];
                fin.setAttribute('min', fechaMin);
            } else {
                fin.removeAttribute('min');
            }
        });

        document.getElementById('sinFechasBtn').addEventListener('click', function() {
            inicio.value = '';
            fin.value = '';
            fin.removeAttribute('min'); 
        });
    }
});
