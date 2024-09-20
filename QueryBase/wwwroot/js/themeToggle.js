let theme = localStorage.getItem('theme');

//$(document).ready(() => {
//    if (theme) {
//        $('body').attr('data-bs-theme', theme);
//    }
//    else {
//        localStorage.setItem('theme', 'light')
//    }
//    setButtonValueAndLabel();
//});


window.addEventListener('DOMContentLoaded', () => {
    if (theme) {
            $('body').attr('data-bs-theme', theme);
        }
        else {
            localStorage.setItem('theme', 'light')
        }
        setButtonValueAndLabel();
    
})

function handleThemeLabelClick() {
    if ($('#themeButton').prop('checked')) {
        $('#themeButton').prop('checked', false);
        $('#md-themeButton').prop('checked', false);
        handleThemeButtonClick();
    }
    else {
        $('#themeButton').prop('checked', true);
        $('#md-themeButton').prop('checked', true);
        handleThemeButtonClick();
    }
}


function handleThemeButtonClick() {
        if ($('#themeButton').prop('checked') === true) {
            $('body').attr('data-bs-theme', 'dark');
            localStorage.setItem('theme', 'dark');
            $('#md-themeButton').prop('checked', true);
            $('#themeLabel').empty();
            $('#md-themeLabel').empty();
            $('#themeLabel').append('Dark');
            $('#md-themeLabel').append('Dark');
        }
        else {
            $('body').attr('data-bs-theme', 'light');
            localStorage.setItem('theme', 'light');
            $('#md-themeButton').prop('checked', false);
            $('#themeLabel').empty();
            $('#md-themeLabel').empty();
            $('#themeLabel').append('Light');
            $('#md-themeLabel').append('Light');
        }
}
function handleMDThemeButtonClick() {
        if ($('#md-themeButton').prop('checked') === true) {
            $('body').attr('data-bs-theme', 'dark');
            localStorage.setItem('theme', 'dark');
            $('#themeButton').prop('checked', true);
            $('#themeLabel').empty();
            $('#md-themeLabel').empty();
            $('#themeLabel').append('Dark');
            $('#md-themeLabel').append('Dark');
        }
        else {
            $('body').attr('data-bs-theme', 'light');
            localStorage.setItem('theme', 'light');
            $('#themeButton').prop('checked', true);
            $('#themeButton').prop('checked', false);
            $('#themeLabel').empty();
            $('#md-themeLabel').empty();
            $('#themeLabel').append('Light');
            $('#md-themeLabel').append('Light');
        }
}

function setButtonValueAndLabel() {
    if (theme === 'dark') {
        $('#themeButton').prop('checked', true);
        $('#md-themeButton').prop('checked', true);
        /*$('#themeLabel').empty();*/
        $('#themeLabel').append('Dark');
        $('#md-themeLabel').append('Dark');

    }
    else {
        $('#themeButton').prop('checked', false);
        $('#md-themeButton').prop('checked', false);
       /* $('#themeLabel').empty();*/
        $('#themeLabel').append('Light');
        $('#md-themeLabel').append('Light');
    }
}