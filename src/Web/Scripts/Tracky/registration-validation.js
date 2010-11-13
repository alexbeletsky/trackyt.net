$(document).ready(function () {
    $("#signup_form").submit(submit);
}
);

function submit() {
    if ($('#Password').val() != $('#ConfirmPassword').val()) {
        $('#PasswordValidationMessage').html('Confirmed password does not match. Please correct');
        return false;
    }
    return true;
}
