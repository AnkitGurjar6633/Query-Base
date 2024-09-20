
document.getElementById('phoneNumber').addEventListener('keypress', function (e) {
    if (e.which < 48 || e.which > 57) {
        e.preventDefault();
    }
});

$(document).ready(function () {
    $('#phoneNumber').inputmask("9999999999");
});