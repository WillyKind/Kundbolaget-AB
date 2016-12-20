function OrderController(id, method) {
    $.ajax({
        url: "/Order/"+method+"/" + id,
        type: 'POST',
        success: function (data) {
            $("#" + id).fadeOut("slow", function()  {                
            });
        }

    });
}

