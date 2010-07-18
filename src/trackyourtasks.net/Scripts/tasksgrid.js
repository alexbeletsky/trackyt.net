(function($) {
$.fn.tasksgrid = function() {
    var t = {
        table: {},
        header: "<tr><th>#</th><th>Description</th><th>Status</th><th>Actual work</th><th><th></tr>",
        addHeader: function() {
            t.table.append(t.header);
        },
        currentIndex: 1,
        newTaskName: "New task",
        newTaskStatus: "New", 
        newActualWork: "00:00:00", 
        addTaskButton : "+",
        aId : function(index) {
            return "addTask_" + index;
        },
        rowId: function(index) {
            return "row_" + index;
        },
        addTask : function() {
            t.table.append(t.createEmptyRow());
            var row = $("#" + t.rowId(t.currentIndex));

            row.append(t.taskIndex());
            row.append(t.taskDescription());
            row.append(t.taskStatus());
            row.append(t.taskActualWork());
            row.append(t.taskAddButton());
            
            $("#" + t.aId(t.currentIndex)).click(function () { t.addTask(); });

            if (t.currentIndex > 0) {
                $("#" + t.aId(t.currentIndex - 1)).remove();
            }

            t.currentIndex++;
        },
        createEmptyRow: function() {
            return "<tr id=\"" + t.rowId(t.currentIndex) +"\"></tr>"
        },
        createTd: function(value) {
            return "<td>" + value + "</td>";
        },
        craeteA: function() {
            return "<a href=\"#\" id=\"" + t.aId(t.currentIndex) + "\">" + t.addTaskButton + "</a>";
        },
        taskIndex : function() {
            return t.createTd(t.currentIndex); 
        },
        taskDescription: function() {
            return t.createTd(t.newTaskName); 
        },
        taskAddButton: function() {
            return t.createTd(t.craeteA()); 
        },
        taskStatus: function() {
            return t.createTd(t.newTaskStatus); 
        },
        taskActualWork: function() {
            return t.createTd(t.newActualWork);
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