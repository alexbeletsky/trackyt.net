module("sign up functionality tests", {
    setup: function () {
        S.open("Registration");
    }
});

// smoke test
test("page has content", function () {
    ok(S("body *").size(), "There be elements in that there body");
});

// I as user could not login with non correct email
test("email is incorrect", function () {
    // arrange
    S('#Email').type('aaaaa');

    // act
    S('#submit-button').click();

    // assert
    S('#PasswordValidationMessage').visible(function () {
        var message = S('#PasswordValidationMessage li:nth-child(1)').html();

        ok(message == "Email address is not valid", "I as user could not login with non correct email");
    });
});

// I as user could not login with empty password
test("password is empty", function () {
    // arrange
    S('#Email').type('a@a.com');

    // act
    S('#submit-button').click();

    // assert
    S('#PasswordValidationMessage').visible(function () {
        var message = S('#PasswordValidationMessage li:nth-child(1)').html();

        ok(message == "Password is empty", "I as user could not login with empty password");
    });
});

// I as user could not login is my password differs from confirmed password
test("password is empty", function () {
    // arrange
    S('#Email').type('a@a.com');
    S('#Password').type('password');
    S('#ConfirmPassword').type('password-aaa');

    // act
    S('#submit-button').click();

    // assert
    S('#PasswordValidationMessage').visible(function () {
        var message = S('#PasswordValidationMessage li:nth-child(1)').html();

        ok(message == "Confirmed password does not match", "I as user could not login is my password differs from confirmed password");
    });
});

// I as user could not register with same email twice
test("register twice", function () {
    // arrange
    var email = "test" + new Date().getSeconds() + new Date().getMilliseconds() + "@trackyt.net";
    S('#Email').type(email);
    S('#Password').type(email);
    S('#ConfirmPassword').type(email);
    S('#submit-button').click(function () {

        // wait till registration is done
        S.wait();

        // act
        // go back to register page and try to register with same credentials
        S.open("Registration", function () {
            S('#Email').type(email);
            S('#Password').type(email);
            S('#ConfirmPassword').type(email);
            S('#submit-button').click(function () {

                // assert
                S('#PasswordValidationMessage').visible(function () {
                    var message = S('.validation-summary-errors li:nth-child(1)').html();
                    same(message, "Sorry, user with such email already exist. Please register with different email.", "I as user could not register with same email twice");
                });

            });
        });
    });
});



// I as user could successfully register with valid email and password and redirected to dashboard
test("successful registration", function () {
    // arrange
    var email = "test" + new Date().getSeconds() + new Date().getMilliseconds() + "@trackyt.net";
    S('#Email').type(email);
    S('#Password').type(email);
    S('#ConfirmPassword').type(email);

    // act
    S('#submit-button').click(function () {

        // assert
        S('div #tasks').visible(function () {
            ok(true,
                "I as user could successfully register with valid email and password and redirected to dashboard");
        });
    });
});

// I as user could select temporary registration, email would be generated for me, my password would be same as email
test("temporary registration", function () {

    // to start use application I just click "start now" link
    S('#start-now').click();

    // I would be redirected to dashboard
    S('div #tasks').exists(function () {

        // I need to check out my password, so I click to "Account" link
        S('#show-account-details').click();

        // and wait till account details are visible
        S('#account-details').visible(function () {

            // I'm ready to grab my email
            var email = S('#email-value').html();

            // now, I go to sign in page again and try to login with my account
            S.open("Login", function () {
                
                // wait for open
                S.wait(1000);

                S('#Email').type(email);
                S('#Password').type(email);
                S('#submit-button').click();

                // and check that I'm able to proceed dashboard
                S('div #tasks').exists(function () {
                    ok(true, "I'm able to login as temp user");
                });
            });
        });
    });
});


