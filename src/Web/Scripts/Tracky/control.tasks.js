function tasksControl(div, layout) {
    this.div = div;
    this.layout = layout;
    this.tasks = [];
}

tasksControl.prototype = (function () {

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // helpers

    function removeFromArray(array, value) {
        return $.grep(array, function(val) { val != value; });
    }

    function getTaskById(tasks, id) {
        for (var t in tasks) {
            if (tasks[t].id == id) {
                return tasks[t];
            }
        }
        return null;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // private members


    // class task definition
    function task(control, t) {
        this.id = t.id;
        this.ref = 'task-' + this.id ;
        this.control = control;
        this.control.div.append('<div id=' + this.ref + ' class="task"></div>');
        this.div = $('#' + this.ref);

        this.sections = [];

        this.sections['description'] = new description(this, t);
        this.sections['remove'] = new remove(this, t);        
        this.sections['start'] = new start(this, t);
        this.sections['stop'] = new stop(this, t);
        this.sections['timer'] = new timer(this, t);

        if (this.control.layout) {
            this.control.layout(this.div);
        }
    }

    // class task members
    task.prototype = (function() {
        return {
            remove: function() {
                this.div.remove();
            },

            start: function () {
                this.sections['timer'].run();
            }
        }

    })();

    // class description definition
    function description(task, t) {
        this.description = t.description;

        task.div.append('<span class="description">' + this.description + '</span>');
    }

    // class timer definition
    function timer(task, t) {
        this.spent = t.spent;
        this.ref = 'timer-' + t.id;

        this.format = function() {
            var minutes = parseInt(this.spent / 60);
            var seconds = this.spent % 60;

            var formatted = '';
            if (minutes < 10)
                formatted += '0';
            formatted += minutes + ':';
            if (seconds < 10)
                formatted += '0';
            formatted += seconds;
            return formatted;
        }

        this.update = function() {
             $('#' + this.ref).html(this.format());
        }

        task.div.append('<span id="' + this.ref +'" class="timer">' + this.format() + '</span>');
    }

    // class timer members
    timer.prototype = (function() {
        return {
            run: function() {
                var me = this;
        
                if (!this.timerId) {
                    this.timerId = setInterval(function() { me.spent++; me.update(); }, 1000);
                }  
            }
        }

    })();

    function start(task, t) {
        task.div.append('<span class="start"><a href="/tasks/start/' + task.id + '" title="Start">Start</a></span>'); 
    }

    function stop(task, t) {
        task.div.append('<span class="stop"><a class="stop" href="/tasks/stop/' + task.id + '" title="Stop">Stop</a></span>'); 
    }

    function remove(task, t) {
        task.div.append('<span class="delete"><a class="delete" href="/tasks/delete/' + task.id + '" title="Delete">Delete</a></span>'); 
    }

    return {

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // public members

        addTask: function (t) {
            var taskToAdd = new task(this, t);
            this.tasks.push(taskToAdd);
        },

        removeTask: function (id) {
            // remove from array
            var taskToRemove = getTaskById(this.tasks, id);
            if (taskToRemove) {
                this.tasks = removeFromArray(this.tasks, taskToRemove);
                // call remove handler of task
                taskToRemove.remove();
            }
        },

        startTask: function(id) {
            var taskToStart = getTaskById(this.tasks, id);   
            if (taskToStart) {
                taskToStart.start();
            }      
        },

        tasksCount: function () {
            return this.tasks.length;
        },
    };
})();