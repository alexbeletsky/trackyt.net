$(function () {
    module("registration-validation tests",
        {
            setup: function () {
                $("#hidden_test_dom").append($('<input id="Email"/>'));
                $("#hidden_test_dom").append($('<input id="Password"/>'));
                $("#hidden_test_dom").append($('<input id="ConfirmPassword"/>'));

                $("#hidden_test_dom").append($('<div id="PasswordValidationMessage"/>'));
            },
            teardown: function () {
                $("#hidden_test_dom").empty();
            }
        }
        );

    test("email address is not valid", function () {
        // arrange
        $('#Email').val("x");

        // act
        var result = signup_submit();

        // assert
        ok(!result, 'form should not be submitted with invalid email');
    });

    test("email address is not valid 2", function () {
        // arrange
        $('#Email').val("x@");

        // act
        var result = signup_submit();

        // assert
        ok(!result, 'form should not be submitted with invalid email');
    });

    test("email address is not valid 3", function () {
        // arrange
        $('#Email').val("x@a");

        // act
        var result = signup_submit();

        // assert
        ok(!result, 'form should not be submitted with invalid email');
    });

    test("password field is empty", function () {
        // arrange
        $('#Email').val("x@a.net");
        $('#Password').val("");

        // act
        var result = signup_submit();

        // assert
        ok(!result, 'form should be not submitted with password empty');
    });

    test("password field is null", function () {
        // arrange
        $('#Email').val("x@a.net");
        $('#Password').val(null);

        // act
        var result = signup_submit();

        // assert
        ok(!result, 'form should not be submitted with password null');
    });

    test("passwords does not match", function () {
        // arrange
        $('#Email').val("x@a.net");
        $('#Password').val("123");
        $('#ConfirmPassword').val("1234");

        // act
        var result = signup_submit();

        // assert
        ok(!result, 'form should not be submitted with password does not match');
    });

    test("passwords match", function () {
        // arrange
        $('#Email').val("x@a.net");
        $('#Password').val("1234");
        $('#ConfirmPassword').val("1234");

        // act
        var result = signup_submit();

        // assert
        ok(result, 'form should be submitted with password match');
    });

    test("validation messages not empty", function () {
        // arrange
        $('#Email').val("x@a.net");

        // act
        var result = signup_submit();

        // assert
        ok($('#PasswordValidationMessage').html() != "", "in case of errors summary made to PasswordValidationMessage");
    });

}
);