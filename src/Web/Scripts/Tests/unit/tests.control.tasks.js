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
        var control = new tasksControl($('#tasks'));
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
        ok(currentTasks == 1, "task has not been added to ui");
    });

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

    test("remove task from control several tasks", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var tasks = [
            { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
            { id: 1, description: "task 2", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
            { id: 2, description: "task 3", status: 0, createdDate: null, startedDate: null, stoppedDate: null }
            ];

        for (var t in tasks) {
            control.addTask(tasks[t]);
        }

        // act
        control.removeTask(tasks[0].id);

        // assert
        var currentTasks = control.tasksCount();
        same(currentTasks, 2, "2 tasks should remain in control");
    });

    test("remove task from ui", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null };
        control.addTask(task);

        // act
        // after addition of fadeIn/fadeOut effect, now remove of task is async
        stop(1000);
        control.removeTask(task.id);

        setTimeout(function () {
            // assert
            var currentTasks = $('#tasks .task').size();
            ok(currentTasks == 0, "task has not been removed from ui");
            start();
        }, 500);
    });

    test("task has description field", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null };

        // act
        control.addTask(task);

        // assert
        var task = $('#tasks .task:first-child');
        ok(task != null, "could not get task from div");
        var description = task.children('.description');
        ok(description.length == 1, "description span is absent in task");
    });

    test("task has start button", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null, spent: 0 };

        // act
        control.addTask(task);

        // assert
        var task = $('#tasks .task:first-child');
        ok(task != null, "could not get task from div");
        var start = task.children('.start').children('a');
        ok(start.length == 1, "start is absent in task");
    });

    test("task start button href is initialized", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null, spent: 0 };

        // act
        control.addTask(task);

        // assert
        var task = $('#tasks .task:first-child');
        ok(task != null, "could not get task from div");
        var start = task.children('.start').children('a');
        ok(start.length == 1, "start is absent in task");
        var href = start.attr('href');
        same(href, "/tasks/start/0", "task href is incorrect");
    });

    test("task has stop button", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null, spent: 0 };

        // act
        control.addTask(task);

        // assert
        var task = $('#tasks .task:first-child');
        ok(task != null, "could not get task from div");
        var start = task.children('.stop');
        ok(start.length == 1, "stop is absent in task");
    });

    test("task stop button href is initialized", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null, spent: 0 };

        // act
        control.addTask(task);

        // assert
        var task = $('#tasks .task:first-child');
        ok(task != null, "could not get task from div");
        var stop = task.children('.stop').children('a');
        ok(stop.length == 1, "start is absent in task");
        var href = stop.attr('href');
        same(href, "/tasks/stop/0", "task href is incorrect");
    });

    test("task has delete button", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null, spent: 0 };

        // act
        control.addTask(task);

        // assert
        var task = $('#tasks .task:first-child');
        ok(task != null, "could not get task from div");
        var start = task.children('.delete');
        ok(start.length == 1, "delete is absent in task");
    });

    test("task delete button href is initialized", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null, spent: 0 };

        // act
        control.addTask(task);

        // assert
        var task = $('#tasks .task:first-child');
        ok(task != null, "could not get task from div");
        var stop = task.children('.delete').children('a');
        ok(stop.length == 1, "delete is absent in task");
        var href = stop.attr('href');
        same(href, "/tasks/delete/0", "delete href is incorrect");
    });

    test("task timer is initialized", function () {
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null, spent: 77 };

        // act
        control.addTask(task);

        // assert
        var task = $('#tasks .task:first-child');
        ok(task != null, "could not get task from div");
        var timer = task.children('.timer').html();
        same(timer, "0:01:17", "timer has not been initialized");
    });

    test("start task", function () {

        // arrange
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null, spent: 0 };
        control.addTask(task);

        // act
        control.startTask(0);

        // assert
        stop(1500);
        setTimeout(function () {
            var task = $('#tasks .task:first-child');
            ok(task != null, "could not get task from div");
            var timer = task.children('.timer').html();
            same(timer, "0:00:01", "timer has not been started");

            start();
        }, 1000);
    });

    test("stop task", function () {

        // arrange
        var control = new tasksControl($('#tasks'));
        var task = { id: 0, description: "task 1", status: 1, createdDate: null, startedDate: null, stoppedDate: null, spent: 1 };
        control.addTask(task);

        stop(1500);
        setTimeout(function () {
            // act
            control.stopTask(0);

            var task = $('#tasks .task:first-child');
            ok(task != null, "could not get task from div");
            var timer = task.children('.timer').html();
            same(timer, "0:00:02", "timer has not been started");

            start();
        }, 1000);
    });

    test("start all tasks", function () {
        // assert
        var control = new tasksControl($('#tasks'));
        var tasks = [
            { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
            { id: 1, description: "task 2", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
            { id: 2, description: "task 3", status: 0, createdDate: null, startedDate: null, stoppedDate: null }
            ];

        for (var t in tasks) {
            control.addTask(tasks[t]);
        }

        // act
        stop(1000);
        control.startAll();
        setTimeout(function () {
            // assert
            var started = control.startedCount();
            same(started, 3, "all tasks are started");
            start();
        }, 500);
    });

    test("stop all tasks", function () {
        // arrange
        var control = new tasksControl($('#tasks'));
        var tasks = [
            { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
            { id: 1, description: "task 2", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
            { id: 2, description: "task 3", status: 0, createdDate: null, startedDate: null, stoppedDate: null }
            ];

        for (var t in tasks) {
            control.addTask(tasks[t]);
        }

        // act
        stop(1000);
        control.stopAll();
        setTimeout(function () {
            // assert
            var stopped = control.stoppedCount();
            same(stopped, 3, "all tasks are stopped");
            start();
        }, 500);
    });

    test("task set description", function () {
        // arrange
        var control = new tasksControl($('#tasks'));
        var tasks = [
            { id: 0, description: "task 1", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
            { id: 1, description: "task 2", status: 0, createdDate: null, startedDate: null, stoppedDate: null },
            { id: 2, description: "task 3", status: 0, createdDate: null, startedDate: null, stoppedDate: null }
            ];

        for (var t in tasks) {
            control.addTask(tasks[t]);
        }

        // act
        var task = $('#tasks .task').get(0);
        control.setTaskDescription($(task).attr('id'), 'task 1 updated');

        // assert
        ok($(task).children('.description').html() == 'task 1 updated');
    });
});