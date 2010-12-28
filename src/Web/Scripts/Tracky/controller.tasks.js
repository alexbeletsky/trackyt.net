$(function () {

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

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Task control handlers

    $('.start a').live('click', function () {
        var method = $(this).attr('href');
        a.call(method, 'PUT', null, function (r) {
            if (r.success) {
                control.startTask(r.data.id);
            }

        });


        return false;
    });

    $('.stop a').live('click', function () {
        return false;
    });

    $('.delete a').live('click', function () {
        return false;
    });


    function layout(task) {
        task.children('.start').addClass('right');
        task.children('.stop').addClass('right');
        task.children('.delete').addClass('right');
    }

    a.call('/tasks/all', 'GET', undefined, function (r) {

        if (r.success) {
            for (var t in r.data.tasks) {
                control.addTask(r.data.tasks[t]);
            }
        }
    });
});