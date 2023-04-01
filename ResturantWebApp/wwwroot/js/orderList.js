//The variable was created to reload the data table
let dataTable;
$(document).ready(function () {
   dataTable = $('#orderListData').DataTable({
        "ajax": {
           "url": "/api/Order",
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
                                <a href="/Admin/Order/OrderDetails?id${data}" class="btn btn-warning mx-2">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                           </div>`
                },

                "width": "10%"
            }
            
        ],
        "width": "100%"
    });
});


Delete = function (url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#228B22',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (data) {
                        if (data.success) {
                            dataTable.ajax.reload();
                            // success notification
                            toastr.success(data.message);
                        }
                        else {
                            // failure notification
                            toastr.success(data.message);
                        }
                    }
                })
        }
    })
}