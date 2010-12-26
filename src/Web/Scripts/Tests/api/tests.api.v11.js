$(function () {

    // helper
    function createCallUrl(url, apiToken, method, params) {
        var callUrl = url + apiToken + "/" + method;
        for (var p in params) {
            callUrl += "/" + params[p];
        }

        return callUrl;
    }

    module("v11 api tests", {
        // setup method will authenticate to v.1.1. API by calling 'authenticate'
        // it will store apiToken, so rest of tests could reuse that

        setup: function () {
            var me = this;

            this.url = 'api/v1.1/';
            this.apiToken = null;

            // authenticate
            var method = 'authenticate';
            var data = { email: 'tracky@tracky.net', password: '111111' };
            var type = 'POST';

            api_test(this.url + method, type, data, function (result) {
                ok(result.success, method + " method call failed");

                me.apiToken = result.data.apiToken;
                ok(me.apiToken.length == 32, "invalid api token");
            });
        }
    }
    );

    // TODO: test is expect at least one task is present, should be corrected,
    // cause test won't work after resetdb

    test("get all tasks method", function () {
        var method = 'tasks/all';
        var data = null;
        var type = 'GET';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");

            var tasks = result.data.tasks;
            ok(tasks.length >= 1, "tasks has not been returned");
        });
    });

    test("get all tasks returns all required fields", function () {
        var method = 'tasks/all';
        var data = null;
        var type = 'GET';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");

            var tasks = result.data.tasks;
            ok(tasks.length >= 1, "tasks has not been returned");

            var task = result.data.tasks[0];
            ok(task.Id !== undefined, "Id field is absent");
            ok(task.Description !== undefined, "Description field is absent");
            ok(task.Status !== undefined, "Status field is absent");
            ok(task.CreatedDate !== undefined, "CreatedDate field is absent");
            ok(task.StartedDate !== undefined, "StartedDate field is absent");
            ok(task.StoppedDate !== undefined, "StoppedDate field is absent");
        });
    });

    test("create new task method", function () {
        var method = 'tasks/new';
        var data = [{ description: 'new task 1' }, { description: 'new task 2'}];
        var type = 'POST';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");
            ok(result.data != null, "data is null");
            ok(result.data.length == 2, "data does not contain 2 items");
            ok(result.data[0].Id > 0, "id for first item is wrong");
            ok(result.data[1].Id > 0, "id for second item is wrong");
        });
    });

    test("new task returns created datetime", function () {
        var method = 'tasks/new';
        var data = [{ description: 'task with datetime'}];
        var type = 'POST';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");
            ok(result.data != null, "data is null");
            ok(result.data.length == 1, "data does not contain 1 item");

            var createdDate = result.data[0].CreatedDate;
            ok(createdDate != null);
        });
    });

    test("delete task method", function () {
        var me = this;

        var method = 'tasks/all';
        var data = null;
        var type = 'GET';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");

            var taskId = result.data.tasks[0].Id;
            ok(taskId >= 1, "could not get task for deletion");

            var method = 'tasks/delete';
            var data = [{ Id: taskId}];
            var type = 'DELETE';
            var params = [];

            var call = createCallUrl(me.url, me.apiToken, method, params);

            api_test(call, type, data, function (result) {
                ok(result.success, method + " method call failed");
                ok(result.data != null, "data is null");
                ok(result.data.length == 1, "data does not contain 1 item");
            });
        });
    });

    //    //        test("start task method", function () {
    //    //            var date = new Date();
    //    //            var utc = Date.UTC(date.getUTCFullYear(), date.);
    //    //            var s = date;
    //    //        });

});