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

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // private members

    var onTaskAddedHandler = null;

    function task(control, t) {
        this.id = t.id;
        this.ref = 'task-' + this.id ;
        this.control = control;
        this.control.div.append('<div id=' + this.ref + ' class="task"></div>');
        this.div = $('#' + this.ref);

        new description(this, t);
        new remove(this, t);        
        new start(this, t);
        new stop(this, t);
        new timer(this, t);

        if (this.control.layout) {
            this.control.layout(this.div);
        }
    }

    task.prototype.remove = function() {
        this.div.remove();
    }

    function description(task, t) {
        this.description = t.description;

        task.div.append('<span class="description">' + this.description + '</span>');
    }

    function timer(task, t) {
        this.spent = t.spent;

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

        task.div.append('<span class="timer">' + this.format() + '</span>');

    }

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
            
            for(var t in this.tasks) {
                if (this.tasks[t].id == id) {
                    // remove from array
                    var taskToRemove = this.tasks[t];
                    this.tasks = removeFromArray(this.tasks, taskToRemove);
                    // call remove handler of task
                    taskToRemove.remove();
                }
            }

        },

        tasksCount: function () {
            return this.tasks.length;
        },
    };
})();