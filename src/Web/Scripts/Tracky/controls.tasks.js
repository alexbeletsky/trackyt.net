function tasksControl(div) {
    this.div = div;
    this.tasks = [];
}

tasksControl.prototype = (function () {

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // private members

    var onTaskAddedHandler = null;

    function task(control, t) {
        this.id = t.id;
        this.control = control;
        this.control.div.append('<div class="task"></div>');
    }

    task.prototype.remove = function() {

    }


    return {

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        // public members

        addTask: function (t) {
            var taskToAdd = new task(this, t);
            this.tasks.push(taskToAdd);

//            if (onTaskAddedHandler) {
//                onTaskAddedHandler(taskToAdd);
//            }
        },

        removeTask: function (id) {
            
            for(var t in this.tasks) {
                if (this.tasks[t].id == id) {
                    // remove from array
                    var tasksToRemove = this.tasks[t];
                    this.tasks = $.grep(this.tasks, function(val) { val != tasksToRemove; });
                    // call remove handler of task
                    tasksToRemove.remove();
                }
            }

        },

        tasksCount: function () {
            return this.tasks.length;
        },

//        onTaskAdded: function (h) {
//            onTaskAddedHandler = h;
//        },

    };
})();

function createTasksControl() {

    var control = new tasksControl(div);

    return control;
} 