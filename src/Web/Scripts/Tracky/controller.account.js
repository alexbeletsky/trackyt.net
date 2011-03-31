$(function () {

    var url = $('#api').val();
    var token = $('#apiToken').val();

    var a = new api(url, token);

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

    $('#share-tasks').live('click', function () {
        var method = $(this).attr('href');
        a.call(method, 'GET', undefined, function (r) {
            if (r.success) {
                var shareLink = r.data.link;
                $('#navigation-container').after('<div class="share-link">Share link: <input id="share-link-value" type="text" value="' + shareLink + '"/><span class="share-link-close">X</span></div>');
                $('#share-link-value').focus().select();
            }
        });
        return false;
    });

    $('.share-link').live('keyup', function (evt) {
        if (evt.keyCode == 27) {
            removeShareLink();
        }
    });

    $('.share-link').live('keyup', 'ctrl+c', function (evt) {
        removeShareLink();
    });

    $('.share-link-close').live('click', function () {
        removeShareLink();
    });



    function removeShareLink() {
        $('.share-link').fadeOut('fast', function () {
            $(this).remove();
        });
    }
});