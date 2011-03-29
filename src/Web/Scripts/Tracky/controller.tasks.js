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
            updateTaskPositionCallback($(task).attr('id'), position++);
        });
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Dashboard handlers
//    $('#add-task').live('click', function () {
//        var d = $('#task-description').val();
//        if (d) {
//            a.call('/tasks/add', 'POST', { description: d }, function (r) {
//                if (r.success) {
//                    control.addTask(r.data.task);
//                    updateAfterSort();
//                }

//                $('#task-description').val('');
//                $('#task-description').focus();
//            });
//        }
//    });

    $('#task-description').live('keyup', function (e) {
        if (e.keyCode == '13') {
            var d = $('#task-description').val();
            if (d) {
                a.call('/tasks/add', 'POST', { description: d }, function (r) {
                    if (r.success) {
                        control.addTask(r.data.task);
                        updateAfterSort();
                        updateAll();
                    }

                    $('#task-description').val('');
                    $('#tasks').focus();
                });
            }

            e.preventDefault();
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

    $('.done, .all').live('click', function () {
        var method = $(this).attr('href');
        loadTasks(method);

        return false; 
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
            var me = $(this);
            $(this).after('<div id="ui-datepicker-div" class="right"><span class="datepicker"></span></div>');
            $('.datepicker').datepicker({
                dateFormat: 'dd-mm-yy',
                minDate: 0,
                onSelect: function (date, inst) {
                    //me.parent().find('.planned').html(date);
                    $('#ui-datepicker-div').remove();
                    control.setTaskPlannedDate(me.parent().attr('id'), date);
                    updateTaskPlannedDateCallback(me.parent().attr('id'), date);
                }
            });

            $('.ui-datepicker').append('<div class="clear"><div class="clean">Not planned</div><div class="close">X</div></div>');
            $('.ui-datepicker div.clean').die();
            $('.ui-datepicker div.close').die();
            $('.ui-datepicker div.clean').live('click', function () {
                //me.parent().find('.planned').html('');

                $('#ui-datepicker-div').remove();
                control.setTaskPlannedDate(me.parent().attr('id'), '');
                updateTaskPlannedDateCallback(me.parent().attr('id'), '');
            });
            $('.ui-datepicker div.close').live('click', function () {
                $('#ui-datepicker-div').remove();
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
                                updateDone();
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

    $('.done a').live('click', function () {
        var method = $(this).attr('href');

        a.call(method, 'PUT', null, function (r) {
            if (r.success) {
                control.removeTask(r.data.task.id);
                updateDone();
            }
        });        

        return false;
    });


    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Callbacks

    function updateTaskPositionCallback(ref, position) {
        var method = '/tasks/update/' + control.getTaskIdFromReference(ref) + '/position';
        a.call(method, 'PUT', { position: position } , function (r) {}); 
    }

    function updateTaskDescriptionCallback(ref, description) {
        var method = '/tasks/update/' + control.getTaskIdFromReference(ref) + '/description'; 
        a.call(method, 'PUT', { description: description }, function (r) {});
    }

    function updateTaskPlannedDateCallback(ref, plannedDate) {
        var method = '/tasks/update/' + control.getTaskIdFromReference(ref) + '/planneddate'
        a.call(method, 'PUT', { plannedDate: plannedDate } , function (r) {});         
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Layout and initialization

    function updateAll()
    {
        a.call('/tasks/total', 'GET', undefined, function (r) {
            if (r.success) {
                var total = r.data.total;
                $('#project-all').empty().append('<a class="all" href="/tasks/all">All (' + total + ')</a>');
            }
        });
    }

    function updateDone()
    {
        a.call('/tasks/totaldone', 'GET', undefined, function (r) {
            if (r.success) {
                var totalDone = r.data.totalDone;
                $('#project-done').empty().append('<a class="done" href="/tasks/done">Done (' + totalDone + ')</a>');
            }
        });
    }

    function updateTasks(r) {
        if (r.success) {
            control.empty();     
            for (var t in r.data.tasks) {
                control.addTask(r.data.tasks[t]);
            }
        }
    }

    function loadTasks(method) {
        $.blockUI();
        a.call(method, 'GET', undefined, function (r) {
            updateTasks(r);
            $.unblockUI();
        });

    }

    function controllerInit() {
        makeSortable();
        updateAll();
        updateDone();

        loadTasks('/tasks/all');
    }

    controllerInit();
});