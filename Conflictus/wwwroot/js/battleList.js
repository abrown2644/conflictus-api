var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "api/battles",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [         
            { "data": "war.name", "width": "40%" },
            { "data": "title", "width": "30%" },
            { "data": "year", "width": "5%" },
            { "data": "type", "width": "5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center display:flex; justisfy-content:space-between;">
                                <a href="/Battles/Edit?id=${data}" class="badge badge-success text-white" style="cursor:pointer;">Edit</a>
                                <a onclick="Delete('/api/Battles?id=' + ${data})" class="badge badge-danger text-white" style="cursor:pointer;">Delete</a>
                            </div>`;
                }, "width": "20%"
            },
        ],
        "language": {
            "emptyTable": "No battles found."
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