$(function () {
    // Global config
    $.blockUI.defaults.message = null;

    var url = $('#api').val();
    var token = $('#apiToken').val();

    var a = new api(url, token);
    var element = $('#tasks');
    var control = new tasksControl(element);


    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // drag-n-drop

    function makeSortable(div, updateTaskPositionCallback) {
        element.sortable({
            axis: 'y',
            delay: 100,
            opacity: 0.6,
            update: function (event, ui) {
                updateAfterSort();
            }
        }
        );
        element.disableSelection();
    }

    function updateAfterSort() {
        var tasks = element.children('.task');

        var position = 1;
        tasks.each(function (index, task) {
            var id = control.getTaskIdFromReference($(task).attr('id'));
            updateTaskPositionCallback(id, position++);
        });
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Dashboard handlers
    $('#add-task').live('click', function () {
        var d = $('#task-description').val();
        if (d) {
            a.call('/tasks/add', 'POST', { description: d }, function (r) {
                if (r.success) {
                    control.addTask(r.data.task);
                    updateAfterSort();
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

    $('.moveontop').live('click', function () {
        $(this).parent().slideUp(function () {
            $(this).prependTo('#tasks').slideDown(function () {
                updateAfterSort();
            });
        });
    });

    $('.description').live('dblclick', function () {
        var value = $(this).html();
        if ($('#description-edit').length == 0) {
            $(this).html('<input type="text" id="description-edit"/><input type="hidden" id="description-prev"/>');
            $('#description-prev').val(value);
            $('#description-edit').val(value).focus();
        }
    });

    $('.plantodate').live('click', function () {
        if ($('.datepicker').length == 0) {
            $(this).after('<div id="ui-datepicker-div" class="right"><span class="datepicker"></span></div>');
            $('.datepicker').datepicker({
                showButtonPanel: true,
                closeText: 'X',
                onSelect: function (date, inst) {
                    $('#ui-datepicker-div').remove();
                }
            });
        }
    });

    function saveTaskEdit() {
        var currentDescription = $('#description-edit').val();
        var previousDescription = $('#description-prev').val();
        var task = $('#description-edit').parent().parent();
        
        control.setTaskDescription(task.attr('id'), currentDescription);
        if (currentDescription != previousDescription) {
            updateTaskDescriptionCallback(task.attr('id'), currentDescription);
        }

        removeEditControls();
    }

    function cancelTaskEdit() {
        var previousDescription = $('#description-prev').val();
        var task = $('#description-edit').parent().parent();
        
        control.setTaskDescription(task.attr('id'), previousDescription);
        
        removeEditControls();                
    }

    function removeEditControls () {
        $('#description-edit').remove();
        $('#description-prev').remove();
    }

    $('#description-edit').live('keyup', function (e) {
        if (e.keyCode == 13) {
            saveTaskEdit();
        } if (e.keyCode == 27) {
            cancelTaskEdit();
        }
    });

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


    $('#content').live('click', function () {

        $('#ui-datepicker-div').remove();

    });

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Callbacks

    function updateTaskPositionCallback(id, position) {
        var method = '/tasks/update/' + id + '/position/' + position;
        a.call(method, 'PUT', undefined, function (r) {}); 
    }

    function updateTaskDescriptionCallback(ref, description) {
        var method = '/tasks/update/' + control.getTaskIdFromReference(ref) + '/description/' + description;
        a.call(method, 'PUT', undefined, function (r) {});
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Layout and initialization

    // initial load of all tasks
    makeSortable();

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