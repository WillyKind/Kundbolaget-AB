function OrderDetailsController(id, method) {
    $.ajax({
        url: "/OrderDetails/" + method,
        type: 'POST',
        data: {id: id, newAmount: $("#" +"amount"+ id).val()},
        success: function (result) {
            if (result) {
                location.reload(true);
            }
        }
    });
}