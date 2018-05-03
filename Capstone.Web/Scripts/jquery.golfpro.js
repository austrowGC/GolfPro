$(Document).ready(function(){
    $("main").addClass("container");
    $("nav").addClass("navbar navbar-expand-lg navbar-light");
    $("nav ul").addClass("navbar-nav mr-auto list-unstyled list-inline");
    $("nav ul li").addClass("nav-item");

    $("form").parent("div").addClass("poop-deck");
    $("form").parent()
    $("form").children().addClass("form-group");
    $("form button:last-child").addClass("btn btn-primary");
    $("span.field-validation-error").addClass("error");
    $("main div").not(".container, #features ").addClass("well");
    $('.dropdown-toggle').dropdown();

    $("input").addClass("form-control");
    $("#greeting").addClass("col-lg-12 col-md-12 col-sm-12");
    $("#col01").addClass("col-lg-5 col-md-5 col-sm-5");
    $("#col02").addClass("col-lg-2 col-md-2 col-sm-2");
    $("#col03").addClass("col-lg-5 col-md-5 col-sm-5");
    $("ul#organizer").addClass("list-inline list-unstyled");
    $("footer ul").addClass("list-inline list-unstyled");
});