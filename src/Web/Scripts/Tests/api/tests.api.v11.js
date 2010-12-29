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
            ok(task.id !== undefined, "Id field is absent");
            ok(task.description !== undefined, "Description field is absent");
            ok(task.status !== undefined, "Status field is absent");
            ok(task.createdDate !== undefined, "CreatedDate field is absent");
            ok(task.startedDate !== undefined, "StartedDate field is absent");
            ok(task.stoppedDate !== undefined, "StoppedDate field is absent");
        });
    });

    test("task add method", function () {
        var method = 'tasks/add';
        var data = { description: 'new task 1' };
        var type = 'POST';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");
            ok(result.data != null, "data is null");
            ok(result.data.task.id > 0, "id for first item is wrong");
        });
    });

    test("task add returns created datetime", function () {
        var method = 'tasks/add';
        var data = { description: 'task with datetime'};
        var type = 'POST';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");
            ok(result.data.task != null, "data is null");
            ok(result.data.task.createdDate != null);
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

            var taskId = result.data.tasks[0].id;
            ok(taskId >= 1, "could not get task for deletion");

            var method = 'tasks/delete/' + taskId;
            var data = null;
            var type = 'DELETE';
            var params = [];

            var call = createCallUrl(me.url, me.apiToken, method, params);

            api_test(call, type, data, function (result) {
                ok(result.success, method + " method call failed");
                ok(result.data.id != null, "data is null");
            });
        });
    });

    test("start task method", function () {
        var me = this;

        var method = 'tasks/all';
        var data = null;
        var type = 'GET';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");

            var taskId = result.data.tasks[0].id;
            ok(taskId >= 1, "could not get task for deletion");

            var method = 'tasks/start/' + taskId;
            var data = null;
            var type = 'PUT';
            var params = [];

            var call = createCallUrl(me.url, me.apiToken, method, params);

            api_test(call, type, data, function (result) {
                ok(result.success, method + " method call failed");
                ok(result.data != null, "data is null");
                ok(result.data.id == taskId, "id of updated item returned");
            });
        });

    });

    test("stop task method", function () {
        var me = this;

        var method = 'tasks/all';
        var data = null;
        var type = 'GET';
        var params = [];

        var call = createCallUrl(this.url, this.apiToken, method, params);

        api_test(call, type, data, function (result) {
            ok(result.success, method + " method call failed");

            var taskId = result.data.tasks[0].id;
            ok(taskId >= 1, "could not get task for deletion");

            var method = 'tasks/stop/' + taskId;
            var data = null;
            var type = 'PUT';
            var params = [];

            var call = createCallUrl(me.url, me.apiToken, method, params);

            api_test(call, type, data, function (result) {
                ok(result.success, method + " method call failed");
                ok(result.data != null, "data is null");
                ok(result.data.id == taskId, "id of updated item returned");
            });
        });
    });


});