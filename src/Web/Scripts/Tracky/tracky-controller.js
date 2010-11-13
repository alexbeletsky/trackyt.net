$(document).ready(function () {
    var userId = $('#userId').val();
    var api = $('#api').val();


    // Tracky control initialization

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
                $('#task-description'),
                $('#add-task'),
                $('#submit'),
                loadData,
                submitData,
                deleteData
            );

    // Account details window initialization

    $('#account-details').hide();
    $('#show-account-details').click(showAccountDetails);
    $('#close-account-details').click(closeAccountDetails);

    function showAccountDetails() {
        $('#account-details').slideDown('fast');
    }

    function closeAccountDetails() {
        $('#account-details').slideUp('fast');    
    }
}
);
