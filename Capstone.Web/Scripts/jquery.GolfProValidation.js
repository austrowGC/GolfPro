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

    $("#login form").validate({
        debug: true,
        rules: {
            Username: required,
            Password: required
        }
    });

});
