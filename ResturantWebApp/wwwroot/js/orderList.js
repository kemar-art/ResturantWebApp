//The variable was created to reload the data table
let dataTable;
$(document).ready(function () {
    let url = window.location.search;
    if (url.includes("cancelled")) {
        loadList("cancelled");
    }
    else {
        if (url.includes("completed")) {
            loadList("completed");
        }
        else {
            if (url.includes("ready")) {
                loadList("ready");
            }
            else {
                loadList("inprocess");
            }
        }
    }
});


function loadList(param) {
    dataTable = $('#orderListData').DataTable({
        "ajax": {
            "url": "/api/order?status=" + param,
            "type": "GET",
            "datatype": "json"
        },

        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "pickupName", "width": "25%" },
            { "data": "applicationUser.email", "width": "20%" },
            { "data": "orderTotal", "width": "10%" },
            { "data": "pickUpTime", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w75 btn-group">
                                <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-warning mx-2">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                           </div>`
                },

                "width": "10%"
            }

        ],
        "width": "100%"
    });
}