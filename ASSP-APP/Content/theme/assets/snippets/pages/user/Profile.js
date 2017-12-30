//== Class Definition
var SnippetProfile = function () {
    var handleUpdateProfileFormSubmit = function () {
        $('#m_profile_update_submit').click(function (e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    UserName: {
                        required: true

                    }
                }
            });

            if (!form.valid()) {
                return;
            }
            var postData = { 'FullName': $("#FullName").val(), 'UserName': $("#UserName").val(), 'Password': $("#Password").val(), 'Address': $("#Address").val(), 'RoleID': $('input[name=rbtn]:checked').val() };

            btn.addClass('m-loader m-loader--right m-loader--light').attr('disabled', true);

            form.ajaxSubmit({
                url: '/home/login',
                data: postData,
                success: function (response, status, xhr, $form) {


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

                    if (response == "UserAlreadyExists")
                    {
                        $("#UserAlready").show();
                        setTimeout(function () { $("#UserAlready").hide(); }, 5000);
                        $("#UserName").focus();
                    }

                }
            });
        });
    }
    var GetAllEmployees = function () {

        var datatable = $('.m_datatable').mDatatable({
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
                title: "User Name",
                // sortable: 'asc', // default sort
                filterable: false, // disable or enable filtering
                width: 150,
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
    //== Public Functions
    return {
        // public functions
        init: function () {
            handleUpdateProfileFormSubmit();
            handleAddUserFormSubmit();
            GetAllEmployees();
        }
    };
}();

//== Class Initialization
jQuery(document).ready(function () {
    SnippetProfile.init();
});