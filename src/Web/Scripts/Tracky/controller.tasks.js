$(function () {
    // Global config
    $.blockUI.defaults.message = null;

    var url = $('#api').val();
    var token = $('#apiToken').val();

    var a = new api(url, token);
    var control = new tasksControl($('#tasks'), layout);


    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Dashboard handlers
    $('#add-task').live('click', function () {
        var d = $('#task-description').val();
        if (d) {
            a.call('/tasks/add', 'POST', { description: d }, function (r) {
                if (r.success) {
                    control.addTask(r.data.task);
                }

                $('#task-description').val('');
                $('#task-description').focus();
            });
        }
    });

    $('#task-description').live('keyup', function (e) {
        if (e.keyCode == '13') {
            e.preventDefault();
            $('#add-task').click();
        }
    });

    $('#start-all').live('click', function () {
        a.call('/tasks/start/all', 'PUT', null, function (r) {
            if (r.success) {
                control.startAll();
            }
        });
    });

    $('#stop-all').live('click', function () {
        a.call('/tasks/stop/all', 'PUT', null, function (r) {
            if (r.success) {
                control.stopAll();
            }
        });
    });

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Task control handlers

    $('.start a').live('click', function () {
        var method = $(this).attr('href');
        a.call(method, 'PUT', null, function (r) {
            if (r.success) {
                control.startTask(r.data.task.id);
            }
        });

        return false;
    });

    $('.stop a').live('click', function () {
        var method = $(this).attr('href');
        a.call(method, 'PUT', null, function (r) {
            if (r.success) {
                control.stopTask(r.data.task.id);
            }
        });

        return false;
    });

    $('.delete a').live('click', function () {
        var method = $(this).attr('href');
        
        $.confirm({
            message: 'Are you sure to delete this task?',
            buttons: {
                Yes: {
                    action: function () {
                        a.call(method, 'DELETE', null, function (r) {
                            if (r.success) {
                                control.removeTask(r.data.id);
                            }
                        });
                    }
                },
                No: {
                
                }
                },
        });

        return false;
    });

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Layout and initialization

    function layout(task) {
        task.children('.start').addClass('right');
        task.children('.stop').addClass('right');
        task.children('.delete').addClass('right');
    }

    // initial load of all tasks
    $.blockUI();
    a.call('/tasks/all', 'GET', undefined, function (r) {
        if (r.success) {
            for (var t in r.data.tasks) {
                control.addTask(r.data.tasks[t]);
            }
        }

        $.unblockUI();
    });
});