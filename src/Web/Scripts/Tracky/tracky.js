    //TODO: BUG - if new task is created, updated, submited.. and deleted then (with no reload) 
    //  it won't be deleted on server, because Id remains null

    //TODO: optimize - just created and deleted tasks (e.g. id = 0) should not be send for deletion

   // $.fn.tracky = function (newTaskDescription, submitTaskButton, submitDataButton, loadData, submitData, deleteData) {

function create_tracky(tasksDiv, newTaskDescription, submitTaskButton, submitDataButton, loadData, submitData, deleteData) {
    // Global config
    $.blockUI.defaults.message = null;

    function tracky(tasksDiv) {

        var object = this;

        this.tasksDiv = tasksDiv;
        this.tasks = [];

        this.submitDataButton = submitDataButton;

        //handle events
        submitTaskButton.click(onSubmitTaskClicked);
        submitDataButton.click(onSubmitDataClicked);

        // handlers
        this.loadData = loadData;
        this.submitData = submitData;
        this.deleteData = deleteData;

        // load table data
        $.blockUI();
        this.loadData(onDataLoaded);

        function onDataLoaded(response) {
            if (response.success) {
                createTasks(response.data.tasks);
            }

            $.unblockUI();
        }

        function createTasks(data) {
            for (var key in data) {
                object.addTask(data[key]);
            }
        }

        function onSubmitTaskClicked() {
            var taskDescription = newTaskDescription.val();
            if (taskDescription) {
                object.addTask(null, taskDescription);
                newTaskDescription.val('');
                newTaskDescription.focus();
            }
        }

        function onSubmitDataClicked() {
            object.submit();
        }
    }

    tracky.prototype.submit = function () {
        var tracky = this;

        // stop all tasks.. and submit
        this.stopAll();

        function getDirtyTasks() {
            var dirty = [];
            for (var task in tracky.tasks) {
                if (tracky.tasks[task].markedDirty()) {
                    dirty.push(tracky.tasks[task]);
                }
            }
            return dirty;
        }

        var dirtyTasks = getDirtyTasks();
        var dataToSubmit = [];
        var dataToDelete = [];

        for (var task in dirtyTasks) {
            if (dirtyTasks[task].markedRemove()) {
                dataToDelete.push(dirtyTasks[task].object());
            }
            else {
                dataToSubmit.push(dirtyTasks[task].object());
            }
        }

        $.blockUI();

        if (dataToSubmit.length > 0) {
            tracky.submitData(dataToSubmit, onDataSubmitted);
        } else {
            onDataSubmitted();
        }

        function onDataSubmitted() {
            if (dataToDelete.length > 0) {
                tracky.deleteData(dataToDelete, onCompleted);
            } else {
                onCompleted();
            }
        }

        function onCompleted() {
            for (var task in dirtyTasks) {
                dirtyTasks[task].resetDirty();
            }

            $.unblockUI();
        }
    }

    tracky.prototype.addTask = function (data, desc) {
        this.tasks.push(new task(this, data, this.tasks.length, desc));
    }

    tracky.prototype.removeTask = function (task) {
        delete this.tasks[task];
        $('#' + task.id).remove();
    }

    tracky.prototype.onMarkedDirty = function (task, set) {
        if (set) {
            this.submitDataButton.addClass('light');
        }
        else {
            this.submitDataButton.removeClass('light');
        }
    }

    tracky.prototype.startAll = function () {
        for (var task in this.tasks) {
            this.tasks[task].start();
        }
    }

    tracky.prototype.stopAll = function () {
        for (var task in this.tasks) {
            this.tasks[task].stop();
        }
    }

    function task(tracky, data, index, desc) {
        this.id = 'taskDiv-' + index;
        this.data = data;
        this.tracky = tracky;
        this.sections = [];
        this.description = desc;
        //this.dirty = data == null ? true : false;
        this.dirty = false;
        this.markedForRemove = false;
        this.started = false;

        tracky.tasksDiv.append('<div id="' + this.id + '" class="task"></div>');
        this.div = $('#' + this.id);
        this.div.hide();

        //div sections
        this.sections['index'] = new indexSection(tracky, this, index, this.sections.length);
        this.sections['description'] = new descriptionSection(tracky, this, index, this.sections.length);
        //this.sections.push(new statusSection(tracky, this, index, this.sections.length));
        this.sections['delete'] = new deleteSection(tracky, this, index, this.sections.length);
        this.sections['start'] = new startSection(tracky, this, index, this.sections.length);
        this.sections['timer'] = new actualWorkSection(tracky, this, index, this.sections.length);

        // in case if new task added, it should be marked dirty
        if (!data) {
            this.markDirty();
        }

        this.div.fadeIn('fast');
    }

    task.prototype.markDirty = function () {
        this.dirty = true;
        this.tracky.onMarkedDirty(this, true);
    }

    task.prototype.markedDirty = function () {
        return this.dirty;
    }

    task.prototype.object = function () {
        var object = {};

        //set hidden fields (0 - newly created task)
        object['Id'] = this.data == null ? 0 : this.data['Id'];

        //set fields from UI
        for (var section in this.sections) {
            if (this.sections[section].property != undefined &&
                        this.sections[section].value != undefined) {
                object[this.sections[section].property()] = this.sections[section].value();
            }
        }
        return object;
    }

    task.prototype.resetDirty = function () {
        this.dirty = false;
        this.tracky.onMarkedDirty(this, false);
    }

    task.prototype.remove = function () {
        this.markedForRemove = true;
        var task = this;
        this.div.fadeOut("slow", function () { task.tracky.removeTask(task); });
    }

    task.prototype.markedRemove = function () {
        return this.markedForRemove;
    }

    task.prototype.isStarted = function () {
        return this.started;
    }

    task.prototype.start = function () {
        this.started = true;
        this.sections['timer'].start();
        this.sections['start'].onStarted();
    }

    task.prototype.stop = function () {
        this.started = false;
        this.sections['timer'].stop();
        this.sections['start'].onStoped();
    }

    task.prototype.startOrStop = function () {
        if (!this.started) {
            this.start();
        } else {
            this.stop();
        }
    }

    function indexSection(tracky, task, index, position) {
        this.id = 'indexSection-' + index;
        this.index = task.data == null ? index : task.data[this.property()];

        task.div.append('<span class="index">#' + this.index + '</span>');
    }

    indexSection.prototype.property = function () {
        return 'Number';
    }

    indexSection.prototype.value = function () {
        return this.index;
    }

    function descriptionSection(tracky, task, index, position) {
        this.id = 'descriptionSection-' + index;
        this.description = task.data == null ? task.description : task.data[this.property()];

        task.div.append('<span class="description">' + this.description + '</span>');
    }

    descriptionSection.prototype.property = function () {
        return 'Description';
    }

    descriptionSection.prototype.value = function () {
        return this.description;
    }


    function statusSection(tracky, task, index, position) {
        this.id = 'statusSection-' + index;
        var status = task.data == null ? 0 : task.data[this.property()] + 0;

        task.div.append('<span>' + createSelect(this.id) + '</span>');
        $('select#' + this.id).change(onChanged);

        //set selected
        var options = $('select#' + this.id + ' option');
        $(options[status]).attr('selected', 'true');

        function createSelect(id) {
            var select =
                    '<select id="' + id + '">'
                        + '<option value="0">New</option>'
                        + '<option value="1">In progress</option>'
                        + '<option value="2">Done</option>'
                    + '</select>';
            return select;
        }

        function onChanged() {
            task.markDirty();
        }
    }

    statusSection.prototype.property = function () {
        return 'Status';
    }

    statusSection.prototype.value = function () {
        var selected = 0;
        var options = $('select#' + this.id + ' option');

        for (var index = 0; index < options.length; index++) {
            if ($(options[index]).attr('selected')) {
                selected = index;
            }
        }

        return selected;
    }

    function actualWorkSection(tracky, task, index, position) {
        this.id = 'actualWork-' + index;
        this.counter = task.data == null ? 0 : task.data[this.property()] + 0;
        this.period = 1000;
        this.timerId = null;
        this.task = task;

        task.div.append('<span class="timer">' + createCounter(this.id) + '</span>');
        this.updateTimer();

        function createCounter(id, value) {
            return '<span id="' + id + '"></span>';
        }
    }

    actualWorkSection.prototype.start = function () {
        var obj = this;
        obj.counter++;
        obj.updateTimer();

        if (!obj.task.markedDirty()) {
            obj.task.markDirty();
        }

        if (obj.timerId == null) {
            obj.timerId = setInterval(function () { obj.start(); }, obj.period);
        }
    }

    actualWorkSection.prototype.updateTimer = function () {
        $('#' + this.id).html(this.formatValue());
    }

    actualWorkSection.prototype.formatValue = function () {
        var minutes = parseInt(this.counter / 60);
        var seconds = this.counter % 60;

        var formatted = '';
        if (minutes < 10)
            formatted += '0';
        formatted += minutes + ':';
        if (seconds < 10)
            formatted += '0';
        formatted += seconds;
        return formatted;
    }

    actualWorkSection.prototype.stop = function () {
        clearTimeout(this.timerId);
        this.timerId = null;
    }

    actualWorkSection.prototype.property = function () {
        return 'ActualWork';
    }

    actualWorkSection.prototype.value = function () {
        return this.counter;
    }

    function startSection(tracky, task, index, position) {
        this.id = 'start-' + index;
        this.ref = 'a#' + this.id;

        task.div.append('<span class="start right">' + createButton(this.id) + '</span>');
        $(this.ref).click(onClick);

        function createButton(id) {
            return '<a href="#" id=' + id + ' >Start</a>';
        }

        function onClick() {
            task.startOrStop();
        }
    }

    startSection.prototype.onStarted = function () {
        $('#' + this.id).html('Stop');
    }

    startSection.prototype.onStoped = function () {
        $('#' + this.id).html('Start');
    }

    function deleteSection(tracky, task, index, position) {
        this.id = 'delete-' + index;

        task.div.append('<span class="delete right">' + createButton(this.id) + '</span>');
        $('a#' + this.id).click(onClick);

        function createButton(id) {
            return '<a href="#" id=' + id + ' >Delete</a>';
        }

        function onClick() {
            task.markDirty();
            task.remove();
        }
    }

    return new tracky(tasksDiv);
}