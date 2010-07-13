(function($) {

$.fn.tasksgrid = function() {
  
    var t = {
        table : {},
        row : "<tr><td>1</td><td>Some great task</td><td><a href=\"#\">Add task</a></td></tr>",
        addTask : function() {
            t.table.append(t.row);
        }
    }

    return this.each( function() {
        var table = $(this);
        t.table = table;

        // create table header..
        table.append("<tr><th>#</th><th>Task description</th><th>Add task<th></tr>");
        t.addTask();
        $("a").click(function () { t.addTask(); } );
    });

}

})(jQuery);