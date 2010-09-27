$(document).ready(function () {
    $('#tasks').tasksgrid(
                'newTaskName',
                'createTask',
                'submitData',
                loadData,
                submitData,
                deleteData
            );

    function loadData(callback) {
        var userId = $('#userId').val();
        $.post('/API/v1/GetAllTasks/' + userId, null, callback, 'json');
    }

    function submitData(data, callback) {
        $.postJson('/API/v1/Submit', data, callback);
    }

    function deleteData(data, callback) {
        $.postJson('/API/v1/Delete', data, callback);
    }
}
);
