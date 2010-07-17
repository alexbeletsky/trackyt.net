(function($) {
$.fn.tasksgrid = function() {
    var t = {
        table: {},
        header: "<tr><th>#</th><th>Description</th><th>Status</th><th>Actual work</th><th><th></tr>",
        addHeader: function() {
            t.table.append(t.header);
        },
        currentIndex: 0,
        startRow: "<tr class=\"visible: false\">",
        endRow: "</tr>",
        newTaskName: "New task",
        newTaskStatus: "New", 
        newActualWork: "00:00:00", 
        addTaskButton : "Add new task",
        aId : function(index) {
            return "addTask_" + index;
        },
        addTask : function() {
            var row = t.startRow 
                + t.taskIndex() 
                + t.taskDescription() 
                + t.taskStatus()
                + t.taskActualWork()
                + t.taskAddButton() 
                + t.endRow;
                 
            t.table.append(row);

            var selector = "#" + t.aId(t.currentIndex);
            $(selector).click(function () { t.addTask(); });

            if (t.currentIndex > 0) {
                var prevASelector = "#" + t.aId(t.currentIndex - 1);
                $(prevASelector).remove();
            }

            t.currentIndex++;
        },
        createTd: function(value) {
            return "<td>" + value + "</td>";
        },
        craeteA: function() {
            return "<a href=\"#\" id=\"" + t.aId(t.currentIndex) + "\"/a>" + t.addTaskButton;
        },
        taskIndex : function() {
            return t.createTd(t.currentIndex); //"<td> " + (t.currentIndex) + "</td>";       
        },
        taskDescription: function() {
            return t.createTd(t.newTaskName); //"<td>" + (t.newTaskName) + "</td>";
        },
        taskAddButton: function() {
            return t.createTd(t.craeteA()); //"<td>" + "<a href=\"#\" id=\"" + t.aId(t.currentIndex) + "\"/a>" + t.addTaskButton + "</td>"
        },
        taskStatus: function() {
            return t.createTd(t.newTaskStatus); //"<td>" + t.newTaskStatus + "</td>";
        },
        taskActualWork: function() {
            return t.createTd(t.newActualWork); //"<td>" + t.newActualWork + "</td>";
        }
    }

    return this.each( function() {
        var table = $(this);
        
        //init
        t.table = table;
        t.addHeader();
        t.addTask();
    });
}
})(jQuery);