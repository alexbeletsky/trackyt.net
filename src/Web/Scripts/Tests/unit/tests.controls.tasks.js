$(function () {
    module("tasks control tests",
        {
            setup: function () {
                $("#hidden_test_dom").append($('<div id="tasks"/>'));
            },
            teardown: function () {
                $("#hidden_test_dom").empty();
            }
        }
        );

    test("smoke test", function () {

        var control = new tasksControl();
        ok(control != null);
    });

    test("add new task to control", function () {

        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null };

        // act
        control.addTask(task);

        // post
        var currentTasks = control.tasksCount();
        ok(currentTasks == 1, "task has not been added to control");
    });

    test("add new task add tasks ui", function () {

        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null };

        // act
        control.addTask(task);

        // post
        var currentTasks = $('#tasks .task').size();
        ok(currentTasks == 1, "task has not been added to control");
    });

//    test("add new task calls callback", function () {
//        var me = this;

//        // assert
//        var control = new tasksControl($('#tasks'));
//        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null };
//        this.handlerCalled = false;

//        function onTaskAddedHanlder(task) {
//            me.handlerCalled = true;
//        }

//        // act
//        control.onTaskAdded(onTaskAddedHanlder);

//        // assert
//        control.addTask(task);
//        ok(this.handlerCalled, "onTaskAdded has not been called");
//    });

    test("remove task from control", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null };
        control.addTask(task);

        // act
        control.removeTask(task.id);

        // assert
        var currentTasks = control.tasksCount();
        ok(currentTasks == 0, "task has not been removed from control");
    });

    //    test("control calls getAllTasks and create tasks", function () {

    //        function getAllTasks(callback) {
    //            var tasks = [
    //                    { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
    //                    { id: 1, description: "task 2", status: 0, createdDate: null, startedDate: null, stoppedDate: null }
    //                ];

    //            callback(tasks);
    //        }

    //        var control = tasksControl($('#tasks'), getAllTasks);

    //        var tasks = $('#tasks .task').size();
    //        ok(tasks == 2, "2 tasks added");
    //    });

});