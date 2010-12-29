function api(url, token) {
    this.url = url;
    this.token = token;
}

api.prototype = (function () {

    return {

        // TODO: correct function to make it work with optional arguments, so I could run 
        // call (method, type, data, function()) and call (method, type, function()) if no data passed..
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