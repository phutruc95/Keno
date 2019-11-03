$(document).ready(function () {
    $('#image-uploader').change(function () {
        var file = this.files[0];
        var reader = new FileReader();

        reader.readAsDataURL(file);
        reader.onload = function () {
            var dataURL = reader.result;
            $('#image-displayer').attr('src', dataURL);
        };
    });

    $('#image-displayer').on('click', function () {
        $('#image-uploader').trigger('click');
    });

});
