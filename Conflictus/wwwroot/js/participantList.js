var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#PDT_load').DataTable({
        "ajax": {
            "url": "/api/participants",            
            "type": "GET",
            "datatype": "json",            
        },
        "columns": [                       
            {
                "data": "flagUrl",
                "render": function (data) {
                    return `<img class='part_flag' src='${data}'/>`;
                }, "width": "1%"
            },
            { "data": "name", "width": "20%" },            
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Participants/Edit?id=${data}" class="badge badge-success text-white" style="cursor:pointer;">Edit</a>
                                <a onclick="Delete('/api/participants?id=' + ${data})" class="badge badge-danger text-white" style="cursor:pointer;">Delete</a>
                            </div>`;
                }, "width": "5%"
            },
        ],
        "language": {
            "emptyTable": "No participants."
        },
        "width": "100%",
        "iDisplayLength": 50
    })
}

function Delete(url) {
    Swal.fire({
        title: "Are your sure?",
        text: "You cannot undo this action.",
        icon: "warning",
        buttons: true,
        showCancelButton: true,
        confirmButtonText: `Delete`,
        confirmButtonColor: `#dc3545`
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}