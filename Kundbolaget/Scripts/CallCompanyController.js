function CompanyController(id, method) {
    $.ajax({
        url: "/Company/" + method + "/" + id,
        type: 'POST',
        success: function (data) {
            $("#" + id).fadeOut("slow", function () {
            });
        }

    });
}