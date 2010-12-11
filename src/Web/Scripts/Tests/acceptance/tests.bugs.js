module("bugs verification", {
    setup: function () {
        S.open("../../../Login");
    }
});


// https://github.com/alexanderbeletsky/Trackyourtasks.net/issues#issue/1
test("After submit task stopped, but start button is not available", function () {

    // arrage
    // login as user
    S('#Email').type("tracky@tracky.net");
    S('#Password').type("mypass");
    S('#submit-button').click();

    // wait till tasks are ready
    S('#tasks').exists(function () {

        // wait till ajax call finished
        S.wait(function () {

            // add new task
            S('#task-description').click().type("fix issue 1");
            S('#add-task').click();

            // wait till task appeared
            S.wait(function () {
                // get last added task and press start
                var startButton = '#start-' + (S('.task').size() - 1);
                ok(S(startButton).html() == "Start", "task is not started yet, ready to start");

                // now I click start button, so task begins
                S(startButton).click(function () {

                    // check that state of button is changed
                    ok(S(startButton).html() == "Stop", "task is started now, so it could only be stopped");

                    // wait for several seconds and submit task
                    S.wait(function () {

                        S('#submit').click(function () {

                            // after the task is submitted we expect that state of task back to initial
                            ok(S(startButton).html() == "Start", "task state back to initial");

                        });
                    });
                });
            });
        });
    });
});