module("home page tests", {
    setup: function () {
        S.open("http://localhost/tracky/Home");
    }
});

// smoke test
test("page has content", function () {
    ok(S("body *").size(), "There be elements in that there body");
})

test("up navigation is loaded", function() {
	S(".login a").click();	
	ok(false, "failed");
});


