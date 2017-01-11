function ProductController(id, method) {
    console.log(id);
    $.ajax({
        url: "/Product/" + method + "/" + id,
        type: 'POST',
        success: function (data) {
            $("#" + id).fadeOut("slow", function () {
            });
        }

    });
}