$(function () {
    module("v11 api tests");

    var url = 'api/v1.1/';
    var apiToken = null;

    test("authenticate method", function () {
        var method = 'Authenticate';
        var data = { email: 'tracky@tracky.net', password: '111111' };
        var type = 'POST';

        api_test(url, method, type, null, data, function (result) {
            ok(result.success, "call failed");

            apiToken = result.data.apiToken;
            ok(apiToken.length == 32, "invalid api token");
        });
    });

    //    test("add task method", function () {
    //        var method = 'addtask';
    //        var params = ['257ef0b2b382fc2bc43f65e2419c01d3'];

    //        api_test(url, method, type, params, function (result) {



    //        });

    //    });
});