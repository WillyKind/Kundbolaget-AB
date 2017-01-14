function OrderDetailsController(id) {
    $.ajax({
        url: "/OrderDetails/SaveOrderDetail",
        type: 'POST',
        data: {id: id, newAmount: $("#" +"amount"+ id).val()},
        success: function (result) {
            if (result) {
                location.reload(true);
                //parent.history.back();
            }
        }
    });
}