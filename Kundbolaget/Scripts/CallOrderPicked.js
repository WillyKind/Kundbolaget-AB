function OrderPicked(id) {
    $.ajax({
        url: "/Order/OrderPicked/" + id,
        type: 'POST',
        success: function (data) {
            $("#" + id).fadeOut("slow", function()  {                
            });
        }

    });
}
