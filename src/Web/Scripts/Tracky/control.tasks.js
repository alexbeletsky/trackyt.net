function tasksControl(div, layout, updateTaskPositionCallback, updateTaskDescriptionCallback) {
    this.div = div;
    this.layout = layout;
    this.updateTaskPositionCallback = updateTaskPositionCallback;
    this.updateTaskDescriptionCallback = updateTaskDescriptionCallback;
    this.tasks = [];

    this.init();
}


tasksControl.prototype = (function () {

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // helpers
    var TaskStatusNone = 0; var TaskStatusStarted = 1; var TaskStatusStopped = 2;

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // helpers

    function removeFromArray(tasks, task) {
        var me = this;
        this.task = task;

        return $.grep(tasks, function (t) {
            return t.id != me.task.id;
        });
    }

    function getTaskById(tasks, id) {
        for (var t in tasks) {
            if (tasks[t].id == id) {
                return tasks[t];
            }
        }
        return null;
    }

    function idToTaskReference(id) {
        return 'task-' + id;
    }

    function taskReferenceToId(ref) {
        return ref.substr(5);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // drag-n-drop

    function makeSortable(div, updateTaskPosition) {
        var me = this;

        div.sortable({
            axis: 'y',
            delay: 100,
            opacity: 0.6,
            update: function (event, ui) {
                updateAfterSort(div, updateTaskPosition);
            }
        }
        );
        div.disableSelection();
    }

    function updateAfterSort(div, updateTaskPosition) {
        var tasks = div.children('.task');

        var position = 1;
        tasks.each(function (index, task) {
            var id = taskReferenceToId($(task).attr('id'));
            updateTaskPosition(id, position++);
        });
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // private members

    // class task definition
    function task(control, t) {
        var me = this;

        this.id = t.id;
        this.ref = idToTaskReference(this.id);
        this.control = control;
        this.control.div.append('<div id=' + this.ref + ' class="task"></div>');
        this.div = $('#' + this.ref);

        this.sections = [];

        this.sections['moveontop'] = new moveOnTop(this, t);
        this.sections['description'] = new description(this, t);
        this.sections['remove'] = new remove(this, t);
        this.sections['stop'] = new stop(this, t);
        this.sections['start'] = new start(this, t);
        this.sections['timer'] = new timer(this, t);

        // subscribe on timer event
        this.timerStarted = function () {
            me.started = true;
            me.sections['start'].disable();
            me.sections['stop'].enable();
        }

        this.timerStopped = function () {
            me.started = false;
            me.sections['start'].enable();
            me.sections['stop'].disable();
        }

        // initialize timer
        this.sections['timer'].onTimerStarted(this.timerStarted);
        this.sections['timer'].onTimerStopped(this.timerStopped);
        this.sections['timer'].init();

        // call external layout handler to apply custom styles
        if (this.control.layout) {
            this.control.layout(this.div);
        }

        // show it
        this.div.slideDown();
    }

    // class task members
    task.prototype = (function () {
        return {
            remove: function () {
                var me = this;
                this.div.slideUp(function () { me.div.remove(); });
            },

            start: function () {
                this.sections['timer'].run();
            },

            stop: function () {
                this.sections['timer'].pause();
            },

            setDescription: function (desc) {
                this.sections['description'].setDescription(desc);
            },

            updateDescription: function (desc) {
                this.setDescription(desc);
                this.control.updateTaskDescriptionCallback(this.id, desc);
            }
        }

    })();

    // class moveOnTop definition
    function moveOnTop(task, t) {
        task.div.append('<span class="moveontop"></span>');
    }

    // class description definition
    function description(task, t) {
        this.description = t.description;
        this.ref = 'description-' + t.id;

        task.div.append('<span id="' + this.ref + '" class="description">' + this.description + '</span>');
    }

    description.prototype = (function () {

        return {

            setDescription: function (d) {
                $('#' + this.ref).html(d);
            }
        };

    })();

    // class timer definition
    function timer(task, t) {
        this.status = t.status;
        this.spent = t.spent;
        this.ref = 'timer-' + t.id;

        // event handlers
        this.onTimerStartedHandler = null;
        this.onTimerStoppedHandler = null;

        this.format = function () {
            var hours = Math.floor(this.spent / 3600);
            var minutes = Math.floor(this.spent / 60) % 60;
            var seconds = this.spent % 60;

            var formatted = '' + hours + ':';
            if (minutes < 10)
                formatted += '0';
            formatted += minutes + ':';
            if (seconds < 10)
                formatted += '0';
            formatted += seconds;
            return formatted;
        }

        this.update = function () {
            $('#' + this.ref).html(this.format());
        }

        task.div.append('<span id="' + this.ref + '" class="timer">' + this.format() + '</span>');
    }

    // class timer members
    timer.prototype = (function () {
        return {
            run: function () {
                var me = this;

                if (!this.timerId) {
                    this.timerId = setInterval(function () { me.spent++; me.update(); }, 1000);
                }

                if (this.onTimerStartedHandler) {
                    this.onTimerStartedHandler();
                }

                $('#' + this.ref).addClass('run');
            },

            pause: function () {
                clearTimeout(this.timerId); this.timerId = null;

                if (this.onTimerStoppedHandler) {
                    this.onTimerStoppedHandler();
                }

                $('#' + this.ref).removeClass('run');
            },

            init: function () {
                if (this.status == TaskStatusStarted) {
                    this.run();
                } else {
                    this.pause();
                }
            },

            onTimerStarted: function (h) {
                this.onTimerStartedHandler = h;
            },

            onTimerStopped: function (h) {
                this.onTimerStoppedHandler = h;
            }
        }

    })();

    var enableDisable = (function () {
        return {
            enable: function () {
                $('#' + this.ref).show();
            },

            disable: function () {
                $('#' + this.ref).hide();
            }
        }
    })();

    function start(task, t) {
        this.ref = 'start-' + t.id;
        task.div.append('<span id="' + this.ref + '" class="start"><a href="/tasks/start/' + task.id + '" title="Start">Start</a></span>');
    }

    start.prototype = enableDisable;

    function stop(task, t) {
        this.ref = 'stop-' + t.id;
        task.div.append('<span id="' + this.ref + '" class="stop"><a href="/tasks/stop/' + task.id + '" title="Stop">Stop</a></span>');
    }

    stop.prototype = enableDisable;

    function remove(task, t) {
        task.div.append('<span class="delete"><a href="/tasks/delete/' + task.id + '" title="Delete">Delete</a></span>');
    }


    return {

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // public members

        init: function () {
            makeSortable(this.div, this.updateTaskPositionCallback);
        },

        addTask: function (t) {
            var taskToAdd = new task(this, t);
            this.tasks.push(taskToAdd);
        },

        removeTask: function (id) {
            var taskToRemove = getTaskById(this.tasks, id);
            if (taskToRemove) {
                this.tasks = removeFromArray(this.tasks, taskToRemove);
                taskToRemove.remove();
            }
        },

        startTask: function (id) {
            var taskToStart = getTaskById(this.tasks, id);
            if (taskToStart) {
                taskToStart.start();
            }
        },

        stopTask: function (id) {
            var taskToStop = getTaskById(this.tasks, id);
            if (taskToStop) {
                taskToStop.stop();
            }
        },

        startAll: function () {
            for (var t in this.tasks) {
                this.tasks[t].start();
            }
        },

        stopAll: function () {
            for (var t in this.tasks) {
                this.tasks[t].stop();
            }
        },

        // used by unit tests
        startedCount: function () {
            var count = 0;
            for (var t in this.tasks) {
                if (this.tasks[t].started) {
                    count++;
                }
            }
            return count;
        },

        // used by unit tests
        stoppedCount: function () {
            var count = 0;
            for (var t in this.tasks) {
                if (!this.tasks[t].started) {
                    count++;
                }
            }
            return count;
        },

        // used by unit tests
        tasksCount: function () {
            return this.tasks.length;
        },

        updatePositions: function () {
            updateAfterSort(this.div, this.updateTaskPositionCallback);
        },

        updateTaskDescription: function (taskRef, desc) {
            var id = taskReferenceToId(taskRef);
            var task = getTaskById(this.tasks, id);
            task.updateDescription(desc);
        },

        setTaskDescription: function (taskRef, desc) {
            var id = taskReferenceToId(taskRef);
            var task = getTaskById(id);
            task.setDescription(desc);
        }
    };
})();