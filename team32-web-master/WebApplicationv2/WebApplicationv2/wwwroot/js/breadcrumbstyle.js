$(document).ready(function () {
    $("#breadcrumbs li")
        .not(":first-child")
        .not(":last-child")
        .not("#button")
        .hide();
});
$("#showBreadcrumbs").click(function () {
    $("#breadcrumbs li")
        .not(":first-child")
        .not(":last-child")
        .not("#button")
        .toggle(50);
});