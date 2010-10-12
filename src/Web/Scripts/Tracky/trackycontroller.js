$(document).ready(function () {
    var userId = $('#userId').val();
    var api = $('#api').val();

    function loadData(callback) {
        $.post(api + '/GetAllTasks/' + userId, null, callback, 'json');
    }

    function submitData(data, callback) {
        $.postJson(api + '/Submit/' + userId, data, callback);
    }

    function deleteData(data, callback) {
        $.postJson(api + '/Delete/' + userId, data, callback);
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
