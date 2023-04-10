//The variable was created to reload the data table
let dataTable;
$(document).ready(function () {
    dataTable = $('#userList').DataTable({
        "ajax": {
            "url": "/api/user",
            "type": "GET",
            "datatype": "json"
        },

        "columns": [
            { "data": "firstName", "width": "20%" },
            { "data": "lastName", "width": "25%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": { id: "id", LockoutEnd: "LockoutEnd" },
                "render": function (data) {
                    let today = new Date().getTime();
                    let lockout = new Date(data.LockoutEnd).getTime();

                    if (lockout > today) {
                        return `<div class="text-center">
                                <a class="btn btn-success " style="cursor:pointer;" onclick=LockUnlock("${data.id}")>
                                    <i class="fa-solid fa-lock-open"></i> unlcok
                                </a></div>`;
                    }
                    else {
                        return `<div class="text-center">
                                <a class="btn btn-danger " style="cursor:pointer;" onclick=LockUnlock("${data.id}")>
                                    <i class="fa-solid fa-lock"></i> lock
                                </a></div>`;
                    }
                   
                }, "width": "13%"

            }
            
        ],
        "width": "100%"
    });
});


function LockUnlock(id) {
    
    $.ajax({
        type: "POST",
        url: "/api/User",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
               
                // success notification
                toastr.success(data.message);
                 dataTable.ajax.reload();
            }
            else {
                // failure notification
                toastr.success(data.message);
            }
        }

    });
}

