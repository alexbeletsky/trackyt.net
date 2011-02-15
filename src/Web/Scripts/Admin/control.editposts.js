function editPostsControl(div) {
    this.div = div;
    this.posts = [];

    this.div.append('<table class="edit-posts"></table>');
    this.table = $('.edit-posts');
    this.table.hide();
}

editPostsControl.prototype = (function () {
    //http://stackoverflow.com/questions/206384/how-to-format-json-date

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // private members
    function post(control, p) {
        var post = '<tr><td>' + createHref('Edit', 'post/edit/' + p.Url) +
            '</td><td>' + createHref('View', 'post/view/' + p.Url) +
            '</td><td>' + p.Title +
            '</td><td>' + p.CreatedBy +
            '</td><td>' + new Date(parseInt(p.CreatedDate.substr(6))).toDateString() +
            '</td><td>' + createHref('Delete', 'post/delete/' + p.Url) +
            '</td></tr>';

        control.table.append(post);

        function createHref(text, href) {
            return '<a href="' + href + '">' + text + '</a>';
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // public members

    return {

        addPost: function (p) {
            var postToAdd = new post(this, p);
            this.posts.push(postToAdd);
        },

        postsCount: function () {
            return this.posts.length;
        },

        hide: function () {
            this.table.hide();
        },

        show: function () {
            this.table.show();
        }

    };

})();