/// <reference path="../jquery-1.4.4-vsdoc.js" />
/// <reference path="../jquery-ui.js" />
/// <reference path="../jquery.blockUI.js" />
/// <reference path="../jquery.confirm.js" />
/// <reference path="../json2.js" />
/// <reference path="../Api/api.js" />
/// <reference path="../Controls/control.tasks.js" />
/// <reference path="../Controls/control.editposts.js" />

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


    $('.done').live('click', function () {
        var method = $(this).attr('href');
        
        showActions();
        loadTasks(method);

        return false; 
    });


    $('.all').live('click', function () {
        var method = $(this).attr('href');
        
        hideActions();
        loadTasks(method);

        return false; 
    });

    $('.actions-delete-all-done').live('click', function () {
        var method =  $(this).attr('href');
        $.confirm({
            message: 'Are you sure to delete all done task?',
            buttons: {
                Yes: {
                    action: function () {
                        deleteAllDoneTasks(method);
                    }
                },
                No: {
                
                }
            },
        });

        return false;
    });

    $('.actions-undo-all').live('click', function () {
        var method =  $(this).attr('href');
        $.confirm({
            message: 'Are you sure to undo all task?',
            buttons: {
                Yes: {
                    action: function () {
                        undoAllTasks(method);
                    }
                },
                No: {
                
                }
            },
        });

        return false;
    });


    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Task control handlers

    $('.moveontop').live('click', function () {
        var task = $(this).parent();
        
        // fix: if calendar is visible, it would be removed before moving the task
        removeDatetimeDiv();

        if (isTaskOnTop(task)) {
            return;
        }

        task.slideUp(function () {
            $(this).prependTo('#tasks').slideDown(function () {
                updateAfterSort();
            });
        });
    });

    function isTaskOnTop(task) {
        var tasks = element.children('.task');

        var topTaskId = $(tasks[0]).attr('id');
        var currentTaskId = $(task).attr('id');
        return topTaskId == currentTaskId;
    }

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
                    removeDatetimeDiv();

                    control.setTaskPlannedDate(me.parent().attr('id'), date);
                    updateTaskPlannedDateCallback(me.parent().attr('id'), date);
                }
            });
            $('.ui-datepicker').append('<div class="clear"><div class="clean">Not planned</div><div class="close">X</div></div>');
            $('.ui-datepicker div.clean').die();
            $('.ui-datepicker div.close').die();
            $('.ui-datepicker div.clean').live('click', function () {
                removeDatetimeDiv();                
                control.setTaskPlannedDate(me.parent().attr('id'), '');
                updateTaskPlannedDateCallback(me.parent().attr('id'), '');
            });
            $('.ui-datepicker div.close').live('click', function () {
                removeDatetimeDiv();
            });
        }
    });

    function removeDatetimeDiv() {
        $('#ui-datepicker-div').remove();
    }

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
        if (e.keyCode == 13) {      //enter
            saveTaskEdit();
        } if (e.keyCode == 27) {    // esc
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

    // TODO: done/undo have identical code, must be to to common method
    $('.done a').live('click', function () {
        var method = $(this).attr('href');

        a.call(method, 'PUT', null, function (r) {
            if (r.success) {
                control.removeTask(r.data.task.id);
                
                updateAll();
                updateDone();
            }
        });        

        return false;
    });

    $('.undo a').live('click', function () {
        var method = $(this).attr('href');

        a.call(method, 'PUT', null, function (r) {
            if (r.success) {
                control.removeTask(r.data.task.id);
                
                updateAll();
                updateDone();
            }
        });        

        return false;        
    });

    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Main menu

    var detailsShown = false;
    $('#account-details').hide();
    $('#show-account-details').live('click', function () {
        if (!detailsShown) {
            detailsShown = true;
            $('#account-details').slideDown('fast');
        } else {
            detailsShown = false;
            $('#account-details').slideUp('fast');
        }

        return false;
    });

    $('#share-tasks').live('click', function () {
        var method = $(this).attr('href');
        a.call(method, 'GET', undefined, function (r) {
            if (r.success) {
                var shareLink = "http://trackyt.net" + r.data.link;
                $('#navigation-container').after('<div class="share-link">Share link: <input id="share-link-value" type="text" value="' + shareLink + '"/><span class="share-link-close">X</span></div>');
                $('#share-link-value').focus().select();
            }
        });
        return false;
    });

    $('.share-link').live('keyup', function (evt) {
        if (evt.keyCode == 27) {
            removeShareLink();
        }
    });

    $('.share-link').live('keyup', 'ctrl+c', function (evt) {
        removeShareLink();
    });

    $('.share-link-close').live('click', function () {
        removeShareLink();
    });

    function removeShareLink() {
        $('.share-link').fadeOut('fast', function () {
            $(this).remove();
        });
    }


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
            updateAll();

            $.unblockUI();
        });

    }

    function deleteAllDoneTasks(method) {
        a.call(method, 'DELETE', undefined, function (r) {
            if (r.success) {
                control.removeAll();
                updateDone();
            }
        });
    }

    function undoAllTasks(method) {
        a.call(method, 'PUT', undefined, function (r) {
            if (r.success) {
                control.removeAll();
                updateAll();
                updateDone();
            }
        });
    }

    function showActions() {
        $('#actions').show();
    }

    function hideActions() {
        $('#actions').hide();
    }

    function initActions() {
        hideActions();

        $('#action-delete-all').append('<a href="/tasks/delete/alldone" class="actions-delete-all-done">Delete all</a>');
        $('#action-delete-all').append('<a href="/tasks/undo/all" class="actions-undo-all">Undo all</a>');
    }

    function controllerInit() {
        initActions();        
        makeSortable();
        updateDone();

        loadTasks('/tasks/all');
    }

    controllerInit();
});