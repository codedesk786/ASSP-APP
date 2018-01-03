//== Class Definition
var SnippetProfile = function () {
    var handleUpdateProfileByUserID = function () {
        $('#m_profile_update_submit').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');
            form.validate({
                rules: {
                    FullName: {
                        required: true

                    }
                }
            });

            if (!form.valid()) {
                return;
            }
            var postData = $('this').serialize();

            form.ajaxSubmit({
                url: '/User/UpdateEmployeeByUserID',
                data: postData,
                success: function (data) {

                    if (data == "UserAlreadyExists") {
                        $('#UpdateEmployee').attr('data-target', '');
                        $("#UserAlreadyExists").show();
                        setTimeout(function () { $("#UserAlreadyExists").hide(); }, 5000);
                        $("#UserName").focus();
                    }
                    if (data == "success") {
                        // $('#UpdateEmployee').attr('data-target', '#m_modal_4');
                        //  $('#UpdateEmployee').attr('data-toggle', 'modal');
                    }

                }
            });
        });
    }
    var handleUpdateProfileFormSubmit = function () {
        $('#UpdateEmployee').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');
            form.validate({
                rules: {
                    FullName: {
                        required: true

                    },
                    UserName: {
                        required: true
                    },
                    Password: {
                        required: true
                    },
                    Address1: {
                        required: true
                    }
                    ,
                    RoleID: {
                        required: true
                    }
                }
            });

            if (!form.valid()) {
                $('#UpdateEmployee').attr('data-target', '');
                return;
            }
            else
            {
                $('#UpdateEmployee').attr('data-target', '#m_modal_4');
            }
            var postData = $('this').serialize();

            form.ajaxSubmit({
                url: '/User/UpdateEmployeeByUserName',
                data: postData,
                success: function (data) {

                    if (data == "UserAlreadyExists") {
                        $('#UpdateEmployee').attr('data-target', '');
                        $("#UserAlreadyExists").show();
                        setTimeout(function () { $("#UserAlreadyExists").hide(); }, 5000);
                        $("#UserName").focus();
                    }
                    if (data == "success")
                    {
                       // $('#UpdateEmployee').attr('data-target', '#m_modal_4');
                      //  $('#UpdateEmployee').attr('data-toggle', 'modal');
                    }

                }
            });
        });
    }
    var handleAddUserFormSubmit = function () {
        $('#m_add_user_submit').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    FullName: {
                        required: true

                    },
                    UserName: {
                        required: true
                    },
                    Password: {
                        required: true
                    },
                    Address: {
                        required: true
                    }
                    ,
                    RoleID: {
                        required: true
                    }
                }
            });

            if (!form.valid()) {
                return;
            }
            var postData = $('this').serialize();

            form.ajaxSubmit({
                url: '/User/InsertUser',
                data: postData,
                success: function (response, status, xhr, $form) {

                    if (response == "UserAlreadyExists") {
                        $("#UserAlready").show();
                        setTimeout(function () { $("#UserAlready").hide(); }, 5000);
                        $("#UserName").focus();
                    }
                    else if (response == "success")
                    {
                        var url = '/User/AllEmployee';
                        window.location.href = url;
                    }

                }
            });
        });
    }
    var GetAllEmployees = function () {
        

        var datatable = $('.m_datatable2').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/User/GetAllEmployees'
                    }
                },
                pageSize: 10,
                saveState: {
                    cookie: true,
                    webstorage: true
                },
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            },

            // layout definition
            layout: {
                theme: 'default', // datatable theme
                class: '', // custom wrapper class
                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.
                footer: false // display/hide footer
            },

            // column sorting
            sortable: true,

            // column based filtering
            filterable: true,

            pagination: true,

            // columns definition
            columns: [{
                field: "FullName",
                title: "Full Name",
                // sortable: 'asc', // default sort
                filterable: false, // disable or enable filtering
                width: 150,
            },
            {
                field: "UserName",
                title: "User Name",
                // sortable: 'asc', // default sort
                filterable: false, // disable or enable filtering
                width: 150,
            },
            {
                field: "Password",
                title: "Password",
                // sortable: 'asc', // default sort
                filterable: false, // disable or enable filtering
                width: 150,
            },
            {
                field: "Address",
                title: "Address",
                // sortable: 'asc', // default sort
                filterable: false, // disable or enable filtering
                width: 150,
            },
            {
                field: "RoleName",
                title: "Role",
                // sortable: 'asc', // default sort
                filterable: false, // disable or enable filtering
                width: 150,
            }, {

                field: "Actions",
                width: 110,
                title: "Actions",
                sortable: false,
                overflow: 'visible',
                template: function (row) {
                    var dropup = (row.getDatatable().getPageSize() - row.getIndex()) <= 4 ? 'dropup' : 'RoleName';

                    return " <a href='#' data-toggle='confirmation' id='DeleteEmployee' class='m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill' title='Delete '><i  class='la flaticon-cancel'></i></a><button class='btn btn-primary' onclick=AlertDelete('"+row.UserName+"') data-title='Do you agree to the Terms and Conditions?' data-type='warning' data-allow-outside-click='true' data-show-confirm-button='true' data-show-cancel-button='true' data-cancel-button-class='btn-danger' data-cancel-button-text='No, I do not agree' data-confirm-button-text='Yes, I agree' data-confirm-button-class='btn-info'>Popup</button><a href='#' id='EditEmployee' data-toggle='modal' data-target='#m_modal_4' class='m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill' title='Edit '><i class='la la-edit'></i></a>";
                }
            }


            ]

             
        });
        function StatusClasses(status) {
            if (status == 'Pending') {
                return "m-badge--brand";
            }
            else if (status == 'Delivered') {
                return "m-badge--metal";
            }
            else if (status == 'Canceled') {
                return "m-badge--primary";
            }
            else if (status == 'Success') {
                return "m-badge--success";
            }
            else if (status == 'Info') {
                return "m-badge--info";
            }

        }
        var query = datatable.getDataSourceQuery();

        $('#m_form_search').on('keyup', function (e) {
            // shortcode to datatable.getDataSourceParam('query');
            var query = datatable.getDataSourceQuery();
            query.generalSearch = $(this).val().toLowerCase();
            // shortcode to datatable.setDataSourceParam('query', query);
            datatable.setDataSourceQuery(query);
            datatable.load();
        }).val(query.generalSearch);

        $('#m_form_status').on('change', function () {
            // shortcode to datatable.getDataSourceParam('query');
            var query = datatable.getDataSourceQuery();
            query.OrderStatus = $(this).val().toLowerCase();
            // shortcode to datatable.setDataSourceParam('query', query);
            datatable.setDataSourceQuery(query);
            datatable.load();
        }).val(typeof query.Status !== 'undefined' ? query.OrderStatus : '');



        $('#m_form_type').on('change', function () {
            // shortcode to datatable.getDataSourceParam('query');
            var query = datatable.getDataSourceQuery();
            query.Type = $(this).val().toLowerCase();
            // shortcode to datatable.setDataSourceParam('query', query);
            datatable.setDataSourceQuery(query);
            datatable.load();
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_status, #m_form_type').selectpicker();


    };
    var handleDeleteEmployee = function () {
        $(".m-datatable__table").on("click", "#DeleteEmployee", function () {
            if (confirm('Are you sure you want to delete user?')) {
                var UserName = $(this).closest("tr").find('td:eq(1)').text();
                //alert(UserName);
                $.ajax({
                    async: true,
                    url: '/User/DeleteEmployeeByID',
                    data: { 'UserName': UserName },
                    success: function (data) {
                        if (data == "success") {
                            $("#DeleteSuccess").show();
                            setTimeout(function () { $("#DeleteSuccess").hide(); }, 5000);
                            GetAllEmployees();
                        }
                        else if (data == "error") {
                            $("#ErrorDelete").show();
                            setTimeout(function () { $("#ErrorDelete").hide(); }, 5000);
                            GetAllEmployees();
                        }
                    }
                });
            } else {
                // Do nothing!
            }
        });
        $(".m-datatable__table").on("click", "#EditEmployee", function () {
            var UserName = $(this).closest("tr").find('td:eq(1)').text();
            //alert(data.UserName);
            $.ajax({
                async: true,
                url: '/User/GetEmployeeByID',
                data: { 'UserName': UserName },
                type: 'POST',
                success: function (data) {
                    var FullName = data.data["0"].FullName;
                    var UserName = data.data["0"].UserName;
                    var Password = data.data["0"].Password;
                    var Address = data.data["0"].Address;
                    var Role = data.data["0"].RoleID;
                    var UserID = data.data["0"].UserID;
                    $('#FullName').val(FullName);
                    $('#UserName').val(UserName);
                    $('#Password').val(Password);
                    $('#Address').val(Address);
                    $('#UserID').val(UserID);
                    $("input[name=RoleID][value=" + Role + "]").prop('checked', true);
                }
            });
        });
    };
    //== Public Functions
    return {
        // public functions
        init: function () {
            handleUpdateProfileFormSubmit();
            handleAddUserFormSubmit();
            GetAllEmployees();
            handleDeleteEmployee();
            handleUpdateProfileByUserID();
        }
    };
}();

//== Class Initialization
jQuery(document).ready(function () {
    SnippetProfile.init();
    
   
});

function AlertDelete(UserName) {

swal({
            title: "Are you sure?",
            text: "You will not be able to recover this imaginary file!",
            type: "warning",
            showCancelButton: !0,
            confirmButtonClass: "btn-warning",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: 1
}, function () {
         
    $.ajax({
        async: true,
        url: '/User/DeleteEmployeeByID',
        data: { 'UserName': UserName },
        success: function (data) {
            if (data == "success") {
                $("#DeleteSuccess").show();
                setTimeout(function () { $("#DeleteSuccess").hide(); }, 5000);
                $("#m_form_search").trigger("keyup");
            }
            else if (data == "error") {
                $("#ErrorDelete").show();
                setTimeout(function () { $("#ErrorDelete").hide(); }, 5000);
                
               
            }
        }
    });
        });
   
}