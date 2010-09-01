(function ($) {
    $.fn.tasksgrid = function (newTaskDescription, submitTaskButton, loadData, submitData) {

        function tasksgrid(tasksDiv) {
            var object = this;

            this.tasksDiv = tasksDiv;
            this.loadData = loadData;
            this.submitData = submitData;
            this.tasks = [];

            //handle events
            $('#' + submitTaskButton).click(onSubmitTaskClicked);

            function onSubmitTaskClicked() {
                var taskDescription = $('#' + newTaskDescription).val();
                if (taskDescription) {
                    createNewTask(taskDescription);
                    $('#' + newTaskDescription).val('');
                }
            }

            function createNewTask(description) {
                object.tasks.push(new task(object, description, object.tasks.length));
            }
        }

        function task(tasksgrid, desc, index) {
            this.id = 'taskDiv_' + index;
            this.tasksgrid = tasksgrid;
            this.sections = [];
            this.description = desc;

            tasksgrid.tasksDiv.append('<div id="' + this.id + '" class="task"></div>');
            this.div = $('#' + this.id);

            //div sections
            this.sections.push(new indexSection(tasksgrid, this, index, this.sections.length));
            this.sections.push(new descriptionSection(tasksgrid, this, index, this.sections.length));
        }

        function indexSection(tasksgrid, task, index, position) {
            this.id = 'indexSection_' + index;

            task.div.append('<span>' + index + '</span>');
        }

        function descriptionSection(tasksgrid, task, index, position) {
            this.id = 'descriptionSection_' + index;

            task.div.append('<span>' + task.description + '</span>');
        }

        return this.each(function () {
            return new tasksgrid($(this));
        });
    }
})(jQuery);