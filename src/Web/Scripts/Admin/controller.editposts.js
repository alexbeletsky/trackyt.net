$(function () {
    var control = new editPostsControl($('#edit-posts-table'));

    $.ajax(
        {
            url: 'posts',
            type: 'GET',
            dataType: 'json',
            success: function (r) {
                if (r.success) {
                    for (var p in r.data.posts) {
                        control.addPost(r.data.posts[p]);
                    }
                    control.show();
                }
            }
        }
    );



});