$(document).ready(function () {
    // Manejo del modal para agregar/actualizar multas
    $('#modalMultum').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que abrió el modal
        var modal = $(this);

        // Si el botón es de "Actualizar", se obtienen los datos y se cargan en el formulario
        if (button.data('id')) {
            modal.find('#hddIdMulta').val(button.data('id'));
            modal.find('#txtFecha').val(button.data('fecha'));
            modal.find('#txtHora').val(button.data('hora'));
            modal.find('#txtLugar').val(button.data('lugar'));
            modal.find('#txtDistrito').val(button.data('distrito'));
            modal.find('#txtNroSerie').val(button.data('nroserie'));
            modal.find('#txtPlaca').val(button.data('placa'));
            modal.find('#txtMarca').val(button.data('marca'));
            modal.find('#txtModelo').val(button.data('modelo'));
            modal.find('#txtNmotor').val(button.data('nmotor'));
            modal.find('#txtAno').val(button.data('ano'));
            modal.find('#txtColor').val(button.data('color'));
            modal.find('#txtEstado').val(button.data('estado'));
            modal.find('#txtPropietario').val(button.data('propietario'));
            modal.find('#txtEmail').val(button.data('email'));
            modal.find('#txtDirecion').val(button.data('direcion'));
            modal.find('#txtGrua').val(button.data('grua'));
            modal.find('#ddlDeposito').val(button.data('iddeposito'));
            modal.find('#ddlInfraccion').val(button.data('idinfraccion'));
            modal.find('#ddlPersonal').val(button.data('idpersonal'));
            modal.find('#txtEstPago').val(button.data('estpago'));
            modal.find('#txtCodPago').val(button.data('codpago'));
            modal.find('#txtTelefono').val(button.data('telefono'));
            modal.find('#Observaciones').val(button.data('observaciones'));
            modal.find('#txtImagenBase64').val(button.data('imagenbase64'));
        } else {
            // Si es un nuevo registro, se limpia el formulario
            modal.find('form')[0].reset();
            modal.find('#hddIdMulta').val(0);
        }
    });

    // Manejo del botón "Guardar" del modal
    $('#btnGuardarMultum').on('click', function () {
        $('#multumForm').submit();
    });

    // Manejo del modal de confirmación de eliminación
    $('#modalDelete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var modal = $(this);

        // Asigna el ID de la multa a eliminar al formulario de eliminación
        modal.find('#deleteMultumId').val(button.data('id'));
    });


    // Manejo del modal para mostrar la imagen
    $('#imageModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que abrió el modal
        var modal = $(this);
        var imagenBase64 = button.data('imagen'); // Obtiene la imagen en Base64

        // Asigna la imagen en el modal
        modal.find('#modalImage').attr('src', imagenBase64);
    });

    // Submitear el formulario de eliminación cuando se confirma la acción
    $('#deleteForm').on('submit', function () {
        return confirm('¿Estás seguro de que deseas eliminar esta multa municipal?');
    });
});
