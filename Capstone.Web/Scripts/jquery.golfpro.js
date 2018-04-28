$(Document).ready(function(){
    $("header, main").addClass("container")
	$("nav").addClass("navbar navbar-expand-lg navbar-light")
	$("nav ul").addClass("navbar-nav mr-auto list-unstyled list-inline")
	$("nav ul li").addClass("nav-item")

	$("form").parent("div").addClass("container-fluid")
	$("form").children().addClass("form-group")
	$("form button:last-child").addClass("btn btn-primary")
    $("span.field-validation-error").addClass("error")
    $("main div").addClass("well")
});