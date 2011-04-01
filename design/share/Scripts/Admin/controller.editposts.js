$(function () {
    var control = new editPostsControl($('#edit-posts-table'));


    //////////////////////////////////////////////////////////////////////////////////////////////////////
    // Control handlers

    $('.delete a').live('click', function () {
        if (confirm("Are you sure to delete this blog post?")) {
            return true;
        }

        return false;
    });


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