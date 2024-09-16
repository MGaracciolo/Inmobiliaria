// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    $('#inmueblesTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": 6}
        ],
        "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Inmuebles",
            "infoEmpty": "Mostrando 0 a 0 de 0 Inmuebles",
            "infoFiltered": "(Filtrado de _MAX_ total Inmuebles)",
            "search": "Buscador:",
            "zeroRecords": "Sin resultados encontrados",
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
            { "orderable": false, "targets": 3 }
        ],
       "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Propietarios",
            "infoEmpty": "Mostrando 0 a 0 de 0 Propietarios",
            "infoFiltered": "(Filtrado de _MAX_ total Propietarios)",
            "search": "Buscador:",
            "zeroRecords": "Sin resultados encontrados",
            "lengthMenu": "Mostrar _MENU_ Propietarios",
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
            { "orderable": false, "targets": 3 }
        ],
       "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Inquilinos",
            "infoEmpty": "Mostrando 0 a 0 de 0 Inquilinos",
            "infoFiltered": "(Filtrado de _MAX_ total Inquilinos)",
            "search": "Buscador:",
            "zeroRecords": "Sin resultados encontrados",
            "lengthMenu": "Mostrar _MENU_ Inquilinos",
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
            { "orderable": false, "targets": 5 }
        ],
      "language": {
            "emptyTable": "No hay datos",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Contratos",
            "infoEmpty": "Mostrando 0 a 0 de 0 Contratos",
            "infoFiltered": "(Filtrado de _MAX_ total Contratos)",
            "search": "Buscador:",
            "zeroRecords": "Sin resultados encontrados",
            "lengthMenu": "Mostrar _MENU_ Contratos",
            "paginate": {
                 "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
   
});
