// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#dataTable").DataTable({select: true});
});

$(function() {
    $('button[data-toggle="ajax-modal"]').click(function(event) {
        var url = $(this).data('url');
        $.get(url).done(function(data) {
            $('#modal-placeholder').html(data);
            $('#modal-placeholder > .createBranchModal').modal('show');
        });
    });
    $("#modal-placeholder").on('click',
        '[data-save="modal"]',
        function(event) {
            // event.preventDefault();
            var form = $(this).parents('.createBranchModal').find('form');
            var actionUrl = form.attr('action');
            var dataToSend = form.serialize();

            $.post(actionUrl, dataToSend).done(function (data) {
                console.log(data);
                var newBody = $('.text', data);
                $("#modal-placeholder").find('.text').replaceWith(newBody);

                var isValid = newBody.find('[name="IsValid"]').val() == 'True';
                if (isValid) {
                    $("#modal-placeholder").find('.createBranchModal').modal('hide');
                }
            });

        });
});