$(Document).ready(function () {

    $("#register #form0").validate({
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
    })

});
