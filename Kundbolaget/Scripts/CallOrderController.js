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

function CreatePdf(id, method) {
    $.ajax({
        url: "/Order/" + method + "/" + id,
        type: 'POST',
        success: function (data) {
            alert("Pdf skapad på skrivbord");
        }

    });
}

