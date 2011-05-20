(function ($) {
    $.postJson = function (url, data, callback) {
        $.ajax(
            {
                url: url,
                type: 'POST',
                processData: false,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(data),
                dataType: 'json',
                complete: callback
            });
    }
})(jQuery);