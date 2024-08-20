

$(document).ready(function () {

    // Función para actualizar la enumeración
    function updateEnumeration() {
        $('#tblarea tbody tr').each(function (index) {
            $(this).find('td:first').text(index + 1);
        });
    }
    // Llamar a la función al cargar la página para enumerar las filas iniciales
    updateEnumeration();

    $('#modalarea').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que disparó el modal
        var areaId = button.data('areaid'); // Extraer info de atributos data-*
        var areaName = button.data('areaname');
        var modal = $(this);

        if (areaId) {
            modal.find('.modal-title').text('Actualizar Área');
            modal.find('#hddcodarea').val(areaId);
            modal.find('#txtnomarea').val(areaName);
        } else {
            modal.find('.modal-title').text('Agregar Área');
            modal.find('#hddcodarea').val(0);
            modal.find('#txtnomarea').val('');
        }
    });

    $('#btnguardar').click(function () {
        var form = $('#areaForm');
        var actionUrl = form.attr('action');

        $.ajax({
            type: 'POST',
            url: actionUrl,
            data: form.serialize(),
            success: function (data) {
                $('#modalarea').modal('hide');
                location.reload();
            }
        });
    });

    $('#modaldelete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var areaId = button.data('areaid');
        var areaName = button.data('areaname');
        var modal = $(this);

        modal.find('#deleteAreaName').text(areaName);
        modal.find('#deleteAreaId').val(areaId);
    });


});
