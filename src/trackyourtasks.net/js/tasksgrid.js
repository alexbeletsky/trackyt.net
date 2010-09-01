(function ($) {
    $.fn.tasksgrid = function (loadData, submitData) {
        // Global config
        $.blockUI.defaults.message = null;

        // Table object
        function table(tableObj) {
            var table = this;
            this.tableObj = tableObj;
            this.header = new header(this);
            this.rows = [];
            this.buttons = [];
            this.dataSize = 0;
            this.lastRowIndex = 0;

            // handlers
            this.loadData = loadData;
            this.submitData = submitData;

            // load table data
            $.blockUI();
            this.loadData(onDataLoaded);

            function onDataLoaded(data) {
                createTableRows(data);
                createTableButtons();

                $.unblockUI();
            }

            function createTableRows(data) {
                table.dataSize = data.length;
                table.lastRowIndex = data[table.dataSize - 1]['Number'];

                for (var key in data) {
                    table.rows.push(new row(table, data[key], table.rows.length));
                }
            }

            function createTableButtons() {
                table.buttons.push(new submitButton(table, table.buttons.length));
            }
        }

        table.prototype.addEmptyRow = function () {
            this.rows.push(new row(this, null, this.rows.length));
        }

        table.prototype.submit = function () {
            var table = this;

            function getDirtyRows() {
                var dirty = [];
                for (var row in table.rows) {
                    if (table.rows[row].isDirty()) {
                        dirty.push(table.rows[row]);
                    }
                }
                return dirty;
            }

            var dirtyRows = getDirtyRows();
            var dataToSubmit = [];

            for (var row in dirtyRows) {
                dataToSubmit.push(dirtyRows[row].object());
            }

            if (dataToSubmit.length > 0) {
                $.blockUI();
                table.submitData(dataToSubmit, onDataSubmitted);
            }

            function onDataSubmitted() {
                for (var row in dirtyRows) {
                    dirtyRows[row].resetDirty();
                }
                $.unblockUI();
            }
        }

        // Submit button
        function submitButton(table, index) {
            this.id = 'submit_' + index;

            table.tableObj.after('<div>' + createButton(this.id) + '</div>');
            $('input#' + this.id).click(onSubmit);

            function createButton(id) {
                return '<input id="' + id + '" type="button" value="Submit"></input>';
            }

            function onSubmit() {
                table.submit();
            }
        }

        // Table header
        function header(table) {
            this.cells = [];
            this.head = $('thead');

            this.head.append('<tr></tr>');
            this.row = $('thead tr');

            this.cells.push(new textCell(table, this, '#'));
            this.cells.push(new textCell(table, this, 'Description'));
            this.cells.push(new textCell(table, this, 'Status'));
            this.cells.push(new textCell(table, this, 'Work'));

            function applyStyles() {

            }
        }

        // Text cell
        function textCell(table, header, text) {
            header.row.append('<th>' + text + '</th>');
        }

        // Row object
        function row(table, data, index) {
            this.index = index;
            this.cells = [];
            this.data = data;
            this.dirty = (data == null ? true : false);

            table.tableObj.append('<tr id=\"' + this.index + '"></tr>');
            this.rowObj = $('tr#' + this.index);

            //cells
            this.cells.push(new indexCell(table, this, this.index, this.cells.length));
            this.cells.push(new descriptionCell(table, this, this.index, this.cells.length));
            this.cells.push(new statusCell(table, this, this.index, this.cells.length));
            this.cells.push(new actualWorkCell(table, this, this.index, this.cells.length));
            this.cells.push(new startTaskCell(table, this, this.index, this.cells.length));
            this.cells.push(new addTaskCell(table, this, this.index, this.cells.length));

            function applyStyles(row) {
                if (index % 2) {
                    //row.css({ 'background-color': '#dddddd', 'color': '#666666' });
                    row.addClass('zebra');
                }
            }

            applyStyles(this.rowObj);
        }

        row.prototype.removeCell = function (cellIndex) {
            $('#' + this.cells[cellIndex].id).remove();
        }

        row.prototype.markDirty = function () {
            this.dirty = true;
        }

        row.prototype.isDirty = function () {
            return this.dirty;
        }

        row.prototype.resetDirty = function () {
            this.dirty = false;
        }

        row.prototype.object = function () {
            var object = {};

            //set hidden fields
            object['Id'] = this.data == null ? 0 : this.data['Id'];
            object['UserId'] = this.data == null ? 0 : this.data['UserId'];

            //set fields from UI
            for (var cell in this.cells) {
                if (this.cells[cell].isData()) {
                    object[this.cells[cell].property()] = this.cells[cell].value();
                }
            }
            return object;
        }

        // Index cell
        function indexCell(table, row, rowIndex, cellIndex) {
            this.index = row.data == null ? (++table.lastRowIndex) : row.data[this.property()];
            row.rowObj.append('<td>' + this.index + '</td>');
        }

        indexCell.prototype.isData = function () {
            return true;
        }

        indexCell.prototype.property = function () {
            return 'Number';
        }

        indexCell.prototype.value = function () {
            return this.index;
        }

        // Description cell
        function descriptionCell(table, row, rowIndex, cellIndex) {
            this.id = 'decription_' + rowIndex;
            var description = row.data == null ? 'Add task description...' : row.data[this.property()];

            row.rowObj.append('<td>' + createInput(this.id, description) + '</td>');
            $('input#' + this.id).bind("keypress", onChanged);

            function createInput(id, txt) {
                return '<input id=\"' + id + '" type="text" value="' + txt + '"/>';
            }

            function onChanged() {
                row.markDirty();
            }
        }

        descriptionCell.prototype.isData = function () {
            return true;
        }

        descriptionCell.prototype.property = function () {
            return 'Description';
        }

        descriptionCell.prototype.value = function () {
            return $('input#' + this.id).val();
        }

        // Status cell
        function statusCell(table, row, rowIndex, cellIndex) {
            this.id = 'status_' + rowIndex;
            var status = row.data == null ? 0 : row.data[this.property()] + 0;

            row.rowObj.append('<td>' + createSelect(this.id) + '</td>');
            $('select#' + this.id).change(onChanged);

            var options = $('select#' + this.id + ' option');
            $(options[status]).attr('selected', 'true');

            function createSelect(id) {
                var select =
                    '<select id="' + id + '">'
                        + '<option label="New" value="0"></option>'
                        + '<option label="In progress" value="1"></option>'
                        + '<option label="Done" value="2"></option>'
                    + '</select>';
                return select;
            }

            function onChanged() {
                row.markDirty();
            }
        }

        statusCell.prototype.isData = function () {
            return true;
        }

        statusCell.prototype.property = function () {
            return 'Status';
        }

        statusCell.prototype.value = function () {
            var selected = 0;
            var options = $('select#' + this.id + ' option');

            for (var index = 0; index < options.length; index++) {
                if ($(options[index]).attr('selected')) {
                    selected = index;
                }
            }

            return selected;
        }

        // Actual work cell
        function actualWorkCell(table, row, rowIndex, cellIndex) {
            this.id = 'actualWork_' + rowIndex;
            this.counter = row.data == null ? 0 : row.data[this.property()] + 0;
            this.timerId = null;
            this.period = 1000;
            this.row = row;

            row.rowObj.append('<td id=\"' + this.id + '">' + this.counter + '</td>');
        }

        actualWorkCell.prototype.start = function () {
            var obj = this;
            obj.counter++;
            $('td#' + obj.id).html(obj.counter);

            obj.row.markDirty();
            obj.timerId = setTimeout(function () { obj.start(); }, obj.period);
        }

        actualWorkCell.prototype.stop = function () {
            clearTimeout(this.timerId);
        }

        actualWorkCell.prototype.isData = function () {
            return true;
        }

        actualWorkCell.prototype.property = function () {
            return 'ActualWork';
        }

        actualWorkCell.prototype.value = function () {
            return this.counter;
        }

        // Start task cell
        function startTaskCell(table, row, rowIndex, cellIndex) {
            this.id = 'startTask_' + rowIndex;
            this.started = false;

            row.rowObj.append('<td><a id="' + this.id + '" href="#">Start</a></td>');
            $('a#' + this.id).click(onClick);

            function onClick() {
                //TODO: correct hardcoded values
                var actualWorkCell = row.cells[3];
                var label = '';
                if (this.started) {
                    actualWorkCell.stop();
                    this.started = false;
                    label = 'Start';
                } else {
                    actualWorkCell.start();
                    this.started = true;
                    label = 'Stop';
                }

                $('a#' + this.id).html(label);
            }
        }

        startTaskCell.prototype.isData = function () {
            return false;
        }

        // Add task cell
        function addTaskCell(table, row, rowIndex, cellIndex) {
            this.id = 'addTask_' + rowIndex;
            this.linkId = 'addTaskLink_' + rowIndex;

            if (lastRow()) {
                row.rowObj.append('<td id="' + this.id + '"><a id="' + this.linkId + '" href="#">+</a></td>');
                $('a#' + this.linkId).click(onClick);
            }

            function lastRow() {
                return (rowIndex >= table.dataSize - 1);
            }

            function onClick() {
                //remove add task cell from current row.. and add next empty;
                row.removeCell(cellIndex);
                table.addEmptyRow();
            }
        }

        addTaskCell.prototype.isData = function () {
            return false;
        }

        // Plugin main
        return this.each(function () {
            return new table($(this));
        });
    }
})(jQuery);