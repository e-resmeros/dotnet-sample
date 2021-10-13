// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    $("#modalConfirmDelete").on('show.bs.modal', function(event){
        var btn = $(event.relatedTarget);
        var target = btn.data('id');
        var modal = $(this);
        modal.find("#userDevID").val(target);
    });

    $(".btn-toggle-menu").click(function() {
        $("#wrapper").toggleClass("toggled");
    });

    $('.col-search').each( function () {
        var title = $(this).text();
        $(this).html( '<input type="text" class="form-control" placeholder="'+title+'" />' );
    } );

    $('.col-select').each( function () {
        $(this).html( "<select class='browser-default custom-select'><option value='ALL' selected>All</option><option value='A'>Active</option><option value='I'>Inactive</option></select>");
    } );

    var table = $('#useListTable').DataTable( {
        serverSide: true,
        ajax: {
            url: "/userDevice/all",
            type: "POST",
            dataType: "json",
            dataSrc: function (d) {
                $('#alertDanger').hide();
                return d.data;
            },
            error : function (response) {
                var button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                $('#alertDanger').html(response.responseJSON.message + button).show();
            },

        },
        columns: [
            { data: "index",
                width: "5%"},
            { data: "employeeNo",
                width: "15%" },
            { data: "companyName",
                width: "20%" },
            { data: "employeeName",
                width: "20%" },
            { render: function (data, type, full, meta) {
                return null != data ? data.toString("yyyy-MM-dd hh:mm:ss") : "";
            }, data: "lastLoginDate",
                width: "15%"},
            { render: function (data, type, full, meta) {
                return data == "A" ? "Active" : "Inactive";
            }, data: "status",
                width: "10%" },
            { render: actionlinks,
                data: "userDeviceID",
                width: "10%"}    
        ],
        language: {
            "decimal":        "",
            "emptyTable":     "No data available in table.",
            "info":           "_TOTAL_ entries found.",
            "decimal":        "",
        },
        initComplete: function () {
            // Apply the search
            this.api().columns().every( function () {
                var that = this;
                $( 'input', this.header() ).on( 'keyup change clear', function () {
                    if ( that.search() !== this.value ) {
                        that
                            .search( this.value )
                            .draw('page');
                    }
                } );

                $( 'select', this.header() ).on( 'change', function () {
                    if ( that.search() !== this.value ) {
                        that
                            .search( this.value )
                            .draw('page');
                    }
                } );

                $('select', this.header()).val('A').trigger('change');
            } );
        },
        autoWidth: false,
        scrollY: "70vh",
        scroller: {
            loadingIndicator: true
        },
        paging: true,
        scrollCollapse: true,
        deferRender: true,
        searching: true,
        lengthChange: false,
        ordering: false,
        dom: '<"top"i>lfrt'
    });

    $('#useListTable')
    .on( 'error.dt', function ( e, settings, techNote, message ) {
        console.log( 'An error has been reported by DataTables: ', message );
    } )
    .DataTable();

    function actionlinks(data, type, full) {
        if (null != data)
            return "<a class='btn-floating btn-sm' data-toggle='modal' data-target='#modalConfirmDelete' data-id='" + data + "'><i class='fas fa-unlink'></i></a>";
        else
            return "";
     
    }
})