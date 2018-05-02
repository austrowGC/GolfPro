$(Document).ready(function(){
    $("header, main").addClass("container")
	$("nav").addClass("navbar navbar-expand-lg navbar-light")
	$("nav ul").addClass("navbar-nav mr-auto list-unstyled list-inline")
	$("nav ul li").addClass("nav-item")

    $("form").parent("div").addClass("poop-deck")
    $("form").parent()
	$("form").children().addClass("form-group")
	$("form button:last-child").addClass("btn btn-primary")
    $("span.field-validation-error").addClass("error")
    $("main div").not(".container").addClass("well")
    $('.dropdown-toggle').dropdown();

    $("input").addClass("form-control");
    $("#greeting").addClass("col-lg-12 col-md-12 col-sm-12")
    $("#col01").addClass("col-lg-6 col-md-6 col-sm-6");
    $("#col02").addClass("col-lg-6 col-md-6 col-sm-6");
});