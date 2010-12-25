function api_test(url, method, type, params, data, callback) {
    var urlWithParams = url;

    urlWithParams += method;
    for (var p in params) {
        urlWithParams += '/' + params[p];
    }

    stop(2000);
    $.ajax(
        {
            url: urlWithParams,
            type: type,
            processData: false,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            dataType: 'json',
            complete: function (result) {
                if (result.status == 404) {
                    ok(false, '404 error');
                } else {
                    callback($.parseJSON(result.responseText));
                }

                start();
            }
        });
}