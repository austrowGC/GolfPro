$(Document).ready(function () {

    $("#register form").validate({
        debug: false,
        rules: {
            FirstName: {
                required: true,
                minlength: 2
            },
            LastName: {
                required: true,
                minlength: 2
            },
            UserName: {
                required: true,
                minlength: 2
            },
            Password: {
                required: true,
                minlength: 8,
                maxlength: 128
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            }
        },
        messages: {
            FirstName: {
                required: "First Name is required",
                minlength: "First Name must have at least two characters"
            },
            LastName: {
                required: "Last Name is required",
                minlength: "Last Name must have at least two characters"
            },
            UserName: {
                required: "User Name is required",
                minlength: "User Name must have at least two characters"
            },
            Password: {
                required: "Password is required",
                minlength: "Password must be at least eight characters",
                maxlength: "Password must be less than 128 characters"
            },
            ConfirmPassword: {
                required: "Confirm Password is required",
                equalTo: "Passwords must match"
            }
        }
    });

    $("#createCourse form").validate({
        debug: false,
        rules: {
            Name: {
                required: true,
                minlength: 2
            },
            LengthInYards: {
                required: true,
                min: 900,
                max: 9999
            },
            Par: {
                required: true,
                min: 13,
                max: 99
            }
        },
        messages: {
            Name: {
                required: "Course Name is required",
                minlength: "Course Name must have at least two characters"
            },
            LengthInYards: {
                required: "Length in Yards is required",
                min: "Value must be greater than 900 and less than 9999",
                max: "Value must be greater than 900 and less than 9999"
            },
            Par: {
                required: "Par is required",
                min: "Value must be greater than 13 and less than 99",
                max: "Value must be greater than 13 and less than 99"
            }

        }

    });

    $("#login form").validate({
        debug: false,
        rules: {
            Username: required,
            Password: required
        }
    });

    $("#createLeague form").validate({
        debug: false,
        rules: {
            Name: {
                required: true,
            },
            CourseID: {
                required: true,
            }
        },
        messages: {
            Name: {
                required: "League Name is required"
            },

            CourseID: {
                required: "Select Course is required"
            }
        }
    };
});