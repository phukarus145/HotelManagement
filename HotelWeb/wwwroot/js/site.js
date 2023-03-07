// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.btn-action').click(function () {
    var url = $(this).data("url");
    $.ajax({
        url: url,
        dataType: 'json',
        success: function (res) {

            // get the ajax response data
            var data = res.body;

            // update modal content here
            // you may want to format data or 
            // update other modal elements here too
            $('.modal-body').text(data);

            // show modal
            $('#myModal').modal('show');

        },
        error: function (request, status, error) {
            console.log("ajax call went wrong:" + request.responseText);
        }
    });
});