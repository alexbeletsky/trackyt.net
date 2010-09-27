$(document).ready(function () {
    var userId = $('#userId').val();

    function loadData(callback) {
        $.post('/API/v1/GetAllTasks/' + userId, null, callback, 'json');
    }

    function submitData(data, callback) {
        $.postJson('/API/v1/Submit', data, callback);
    }

    function deleteData(data, callback) {
        $.postJson('/API/v1/Delete', data, callback);
    }

    $('#tasks').tasksgrid(
                'newTaskName',
                'createTask',
                'submitData',
                loadData,
                submitData,
                deleteData
            );

}
);
