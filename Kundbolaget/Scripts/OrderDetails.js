function OrderDetailsController(id, method) {
    var newAmount = $(".row").each(function(i, obj) {
        console.log(obj);
    } )
    console.log(newAmount);
    $.ajax({
        url: "/OrderDetails/" + method + "/" + id + newAmount ,
        type: 'POST',
        success: function (data) {
            $("#" + id).fadeOut("slow", function () {
            });
        }

    });
}
