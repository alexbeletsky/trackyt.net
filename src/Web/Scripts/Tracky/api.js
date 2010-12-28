function api(url, token) {
    this.url = url;
    this.token = token;
}

api.prototype = (function () {

    return {

        call: function (method, type, data, callback) {

            if (method) {
                $.ajax(
                    {
                        url: this.url + this.token + method,
                        type: type,
                        processData: false,
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(data),
                        dataType: 'json',
                        success: function (result) {
                            callback(result);
                        }
                    });
            }
        },
    };

})();