$(document).ready(function () {
    var userId = $('#userId').val();

    function loadData(callback) {
        $.post('/API/v1/GetAllTasks/' + userId, null, callback, 'json');
    }

    function submitData(data, callback) {
        $.postJson('/API/v1/Submit/' + userId, data, callback);
    }

    function deleteData(data, callback) {
        $.postJson('/API/v1/Delete/' + userId, data, callback);
    }

    $('#tasks').tracky(
                'newTaskName',
                'createTask',
                'submitData',
                loadData,
                submitData,
                deleteData
            );

}
);
