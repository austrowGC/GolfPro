$(Document).ready(function () {

    $("#register form").validate({
        debug: false,
        rules: {
            FirstName: {
                required: true,
                minLength: 2


            },
            LastName: {
                required: true,
                minLength: 2

            },
            UserName: {
                required: true,
                minLength: 2
            },
            Password: {
                required: true,
                minLength: 8,
                maxLength: 128
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            }
        }
    });

    $("#createCourse form").validate({
        debug: false,
        rules: {
            Name: {
                required: true,
                minLength: 2

            },
            LengthInYards: {
                required: true,
                minLength: 4,
                maxLength: 5,
                digits: true

            },
            Par: {
                required: true,
                minLength: 2,
                maxLength: 2,
                digits: true
            }
        }
    });

    $("#login form").validate({
        debug: true,
        rules: {
            Username: required,
            Password: required
        }
    });

});