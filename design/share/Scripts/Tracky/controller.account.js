$(function () {

    var detailsShown = false;
    $('#account-details').hide();
    $('#show-account-details').live('click', function () {
        if (!detailsShown) {
            detailsShown = true;
            $('#account-details').slideDown('fast');
        } else {
            detailsShown = false;
            $('#account-details').slideUp('fast');
        }

        return false;
    });
});