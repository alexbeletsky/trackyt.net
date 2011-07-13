$(function () {

    module("test editposttable control",
        {
            setup: function () {
                $('#hidden_test_dom').append('<div id="edit-posts-table">');
            },

            teardown: function () {
                $('#hidden_test_dom').empty();
            }
        });


    test("edit post table initialized", function () {
        // arrange
        var control = new editPostsControl($('#edit-posts-table'));

        // act

        // assert
        var table = $('#edit-posts-table .edit-posts');
        ok(table.length > 0, "post table is not initialized");

    });

    test("add post", function () {
        // arrange
        var post =
            { url: 'post-1-url', title: 'post-1-title', CreatedDate: '/Date(1224043200000)/', publishedDate: '11-11-2011', author: 'Alexander Beletsky' };

        var control = new editPostsControl($('#edit-posts-table'));

        // act
        control.addPost(post);

        // assert
        var currentPosts = control.postsCount();
        ok(currentPosts == 1);
    });

});