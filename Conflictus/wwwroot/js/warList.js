var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#WDT_load').DataTable({
        "ajax": {
            "url": "api/wars",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "date", "width": "10%" },
            { "data": "name", "width": "40%" },            
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center display:flex; justisfy-content:space-between;">
                                <a href="/Wars/Details?id=${data}" class="badge badge-primary text-white" style="cursor:pointer;">View</a>
                                <a href="/Wars/Edit?id=${data}" class="badge badge-success text-white" style="cursor:pointer;">Edit</a>
                                <a onclick="Delete('/api/wars?id=' + ${data})" class="badge badge-danger text-white" style="cursor:pointer;">Delete</a>
                            </div>`;
                }, "width": "10%"
            },
        ],
        "language": {
            "emptyTable": "No wars found."
        },
        "width": "100%",
        "iDisplayLength": 50
    })
}

function Delete(url) {
    Swal.fire({
        title: "Wait! Stop!",
        text: "Deleting this war will ALSO delete every battle associated with it!",
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