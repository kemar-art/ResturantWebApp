//The variable was created to reload the data table
let dataTable;
$(document).ready(function () {
   dataTable = $('#meueItemData').DataTable({
        "ajax": {
            "url": "/api/MenuItem",
            "type": "GET",
            "datatype": "json"
        },

        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w75 btn-group">
                                <a href="/Admin/MenuItems/upsert?id=${data}" class="btn btn-warning mx-2">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                 <a onClick=Delete('/api/MenuItem/'+${data}) class="btn btn-danger mx-2">
                                    <i class="bi bi-trash"></i>
                                </a>
                           </div>`
                },

                "width": "15%"
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