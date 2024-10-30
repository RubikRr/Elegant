// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myFunction(element, id) {
    if (element.style.color == "red") {
        element.style.color = "black";
        element.classList.remove("fa-heart");
        element.classList.add("fa-heart-o");
    }
    else {
        element.style.color = "red";
        element.classList.remove("fa-heart-o");
        element.classList.add("fa-heart");
    }
}
