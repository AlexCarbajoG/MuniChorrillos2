$(document).ready(function () {
    // Función para cargar y bloquear las cocheras ocupadas
    function loadOccupiedCocheras() {
        $.getJSON('/ControlVehicular/GetOccupiedCocheras', function (data) {
            $('#txtCochera option').each(function () {
                var value = parseInt($(this).val());
                if (data.includes(value)) {
                    $(this).attr('disabled', 'disabled');
                } else {
                    $(this).removeAttr('disabled');
                }
            });
        });
    }

    // Script para manejar la apertura del modal de Crear/Actualizar
    $('#modalControlVehicular').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que abre el modal
        var idControl = button.data('id'); // Extrae la información de los atributos data-*

        if (idControl) {
            // Código para edición
            var placa = button.data('placa');
            var hingreso = button.data('hingreso');
            var hsalida = button.data('hsalida');
            var dia = button.data('dia');
            var cochera = button.data('cochera');
            var estado = button.data('estado');

            // Rellena los campos del formulario con los datos extraídos
            $('#IdControl').val(idControl);
            $('#txtPlaca').val(placa);
            $('#txtHingreso').val(hingreso);
            $('#txtHsalida').val(hsalida).prop('readonly', false); // Habilita el campo para edición
            $('#txtDia').val(dia);
            $('#txtCochera').val(cochera).prop('disabled', false); // Permite cambiar la cochera en edición
            $('#txtEstado').val(estado);

            // Cargar cocheras ocupadas
            loadOccupiedCocheras();

            // Cambia el título del modal
            $('#modalLabelControlVehicular').text('Actualizar Control Vehicular');
        } else {
            // Código para nuevo registro
            $('#controlVehicularForm')[0].reset();
            $('#IdControl').val(0);

            // Establecer la fecha actual en el campo "Día"
            var today = new Date().toISOString().split('T')[0];
            $('#txtDia').val(today);

            // Establecer la hora actual en el campo "Hingreso"
            var now = new Date();
            var hours = now.getHours().toString().padStart(2, '0');
            var minutes = now.getMinutes().toString().padStart(2, '0');
            var currentTime = hours + ':' + minutes;
            $('#txtHingreso').val(currentTime);

            // Establecer "00:00" como valor predeterminado para Hsalida y deshabilitarlo
            $('#txtHsalida').val("00:00").prop('readonly', true);

            // Cargar cocheras ocupadas
            loadOccupiedCocheras();

            // Cambia el título del modal
            $('#modalLabelControlVehicular').text('Agregar Control Vehicular');
        }
    });

    // Manejar el botón de guardar en el modal Crear/Actualizar
    $('#btnGuardarControlVehicular').click(function () {
        $('#controlVehicularForm').submit();
    });

    // Script para manejar la apertura del modal de eliminación
    $('#modalDelete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que abre el modal
        var idControl = button.data('id'); // Extrae la información de los atributos data-*

        // Rellena el campo hidden con el ID del registro a eliminar
        $('#deleteControlId').val(idControl);
    });

    // Manejar la apertura del modal de Actualizar Hora de Salida
    $('#modalActualizarHsalida').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Botón que abre el modal
        var idControl = button.data('id'); // Extrae la información de los atributos data-*
        var hsalida = button.data('hsalida'); // Hora de salida actual

        // Rellena el formulario con la hora de salida actual
        $('#IdControl').val(idControl);
        $('#txtHsalidaUpdate').val(hsalida);
    });

    // Manejar el botón de guardar en el modal de Actualizar Hora de Salida
    $('#btnGuardarHsalida').click(function () {
        $('#formActualizarHsalida').submit();
    });
});
