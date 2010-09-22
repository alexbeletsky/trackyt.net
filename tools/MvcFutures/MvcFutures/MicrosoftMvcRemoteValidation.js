Sys.Mvc.ValidatorRegistry.validators.remote = function (rule) {
    var url = rule.ValidationParameters.url;
    var parameterName = rule.ValidationParameters.parameterName;
    var message = rule.ErrorMessage;

    return function (value, context) {
        if (!value || !value.length) {
            return true;
        }

        if (context.eventName != 'blur') {
            return true;
        }

        var newUrl = ((url.indexOf('?') < 0) ? (url + '?') : (url + '&'))
                     + encodeURIComponent(parameterName) + '=' + encodeURIComponent(value);

        var completedCallback = function (executor) {
            if (executor.get_statusCode() != 200) {
                return;
            }

            var responseData = executor.get_responseData();
            if (responseData != 'true') {
                var newMessage = (responseData == 'false' ? message : responseData);
                context.fieldContext.addError(newMessage);
            }
        };

        var r = new Sys.Net.WebRequest();
        r.set_url(newUrl);
        r.set_httpVerb('GET');
        r.add_completed(completedCallback);
        r.invoke();
        return true;
    };
};
