function api_test(url, type, data, callback) {
    $.ajax(
        {
            url: url,
            type: type,
            processData: false,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            dataType: 'json',
            async: false,
            complete: function (result) {
                if (result.status == 0) {
                    ok(false, '0 status - browser could be on offline mode');
                } else if (result.status == 404) {
                    ok(false, '404 error');
                } else {
                    callback($.parseJSON(result.responseText));
                }
            }
        });
}