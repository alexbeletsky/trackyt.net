$().ready(function () {

    $.confirm = function (params) {
        var buttonsMarkup = '';
        $.each(params.buttons, function (name, obj) {
            buttonsMarkup += '<a href="#" class="button">' + name + '<span></span></a>';
            if (!obj.action) {
                obj.action = function () { };
            }
        });

        var markup = [
            '<div id="confirmOverlay">',
            '<div id="confirmBox">',
            '<p>', params.message, '</p>',
            '<div id="confirmButtons">',
            buttonsMarkup,
            '</div></div></div>'
        ].join('');

        $(markup).hide().appendTo('body').fadeIn();

        $('#confirmBox .button').each(function (name, button) {
            $(button).click(function () {
                $.confirm.hide(function () {
                    params.buttons[$(button).text()].action();
                });
                return false;
            });
        });
    };

    $.confirm.hide = function (callback) {
        $('#confirmOverlay').fadeOut(function () {
            $(this).remove();

            if (callback) {
                callback();
            }
        });
    };
});
        