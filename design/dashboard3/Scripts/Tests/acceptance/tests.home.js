module("home page tests", {
    setup: function () {
        S.open("Home");
    }
});

// smoke test
test("page has content", function () {
    ok(S("body *").size(), "There be elements in that there body");
});

// I as user able to see header navigation menu
test("up navigation is loaded", function() {
    ok(S("#navigation").size(), "Home page container up navigation menu");
});


// I as user able to see footer navigation menu
test("footer navigation is loaded", function () {
    ok(S("#navigation-footer").size(), "Home page container footer navigation menu");
});