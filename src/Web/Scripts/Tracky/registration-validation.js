$(document).ready(function () {
    $("#signup_form").submit(signup_submit);
}
);

function signup_submit() {
    var validateEmail = /^.+@.+\..{2,3}$/;
    var result = true;
    var errors = [];

    if (!validateEmail.test($('#Email').val())) {
        result = false;
        errors.push('Email address is not valid');
    }
    
    if ($('#Password').val() == "") {
        result = false;
        errors.push('Password is empty');
    }
    
    if ($('#Password').val() != $('#ConfirmPassword').val()) {
        result = false;
        errors.push('Confirmed password does not match');        
    }

    if (!result) {
        var list = '<ul>';

        for (var error in errors) {
            list += '<li>' + errors[error] + '</li>';
        }

        list += '</ul>';

        $('#PasswordValidationMessage').html('');
        $('#PasswordValidationMessage').append('<span>Values in form is incorrect</span>');
        $('#PasswordValidationMessage').append(list);
    }

    return result;
}
