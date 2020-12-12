$(function () {
   
    $(".datepicker").datetimepicker();
});

function opendescription() {
    document.getElementById("description").style.display = "block";
}

function closedescription() {
    document.getElementById("description").style.display = "none";
}
function openComment() {
    document.getElementById("Comment").style.display = "block";
}

function closeComment() {
    document.getElementById("Comment").style.display = "none";
}