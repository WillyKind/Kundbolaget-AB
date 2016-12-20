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

function ShipOrder(id) {
    $.ajax({
        url: "/Order/OrderShipped/" + id,
        type: 'POST',
        success: function (data) {
            $("#" + id).fadeOut("slow", function () {
            });
        }
    });
}

function DeliverOrder(id) {
    $.ajax({
        url: "/Order/OrderDelivered/" + id,
        type: 'POST',
        success: function (data) {
            $("#" + id).fadeOut("slow", function () {
            });
        }
    });
}
