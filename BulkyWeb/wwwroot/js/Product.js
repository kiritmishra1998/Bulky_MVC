﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/Admin/Product/GetAll'},
        "columns": [
        { data: 'title',"width": "20%" },
        { data: 'isbn',"width":"25%" },
        { data: 'listPrice', "width":"10%" },
        { data: 'author', "width": "15%" },
        { data: 'category.name', "width": "10%" },
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Admin/Product/Upsert?Id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i> EDIT </a>
                    <a onClick =Delete('/Admin/Product/Delete?Id=${data}') class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i> DELETE </a>
                    </div>`
                },
                "width":"25%"
            }
    ]
  });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })

        }
    });
}