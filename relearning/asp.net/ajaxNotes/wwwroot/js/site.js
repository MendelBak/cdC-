
$(document).ready(function () {
    // If editNote button is clicked, display the form that allows the user to modify the note.
    $(".editNote").click(function () {
        // Only display one form at a time
        if ($("#UpdateNote").css("display") === "none") {
            console.log($("#UpdateNote").css("display"));
            $(this).hide()
            $("#UpdateNote").show();
        }
    });

    // Shows for for use to create a new note 
    $("#allowNewNote").click(function () {
        $("#allowNewNote").hide();
        $("#NewNote").show();
    });

    // If updateNoteButton button is clicked make AJAX GET call to refresh values
    $("#updateNoteButton").click(function(){
        $.get("/UpdateNote/@note["id"]"),
    });
});