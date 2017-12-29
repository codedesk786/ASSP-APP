//== Class definition

var DatatableRemoteAjaxDemo = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: '/home/LoadOrders'
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
            filterable: false,

            pagination: true,

            // columns definition
            columns: [{
                field: "ServiceOrders",
                title: "ServiceOrders #",
                // sortable: 'asc', // default sort
                filterable: false, // disable or enable filtering
                width: 150,
            }, {
                field: "BusinessPartnerCode",
                title: "Business Partner Code",
                // sortable: 'asc', // default sort
                filterable: false, // disable or enable filtering
                width: 150,
                },
                {
                field: "BPName",
                title: "BPName",
                width: 150,
                
            },
            {
                field: "Configuration",
                title: "Configuration",
                width: 150,

                },
                {
                    field: "ServiceLocationAddress",
                    title: "ServiceLocationAddress",
                    width: 150,

            },
            {
                field: "OrderDate",
                title: "OrderDate",
                width: 150,

                },
                {
                    field: "EstimatedStartDate",
                    title: "EstimatedStartDate",
                    width: 150,

            },
            {
                field: "EstimatedEndDate",
                title: "EstimatedEndDate",
                width: 150,

                },
                {
                    field: "Duration",
                    title: "Duration",
                    width: 150,

            },
            {
                field: "ActualStartDate",
                title: "ActualStartDate",
                width: 150,

                },
                {
                    field: "ActualFinishDate",
                    title: "ActualFinishDate",
                    width: 150,

            },
            {
                field: "OrderStatus",
                title: "OrderStatus",
                width: 150,

                },
                {
                    field: "ReasonforIntruption",
                    title: "ReasonforIntruption",
                    width: 150,

            },
            {
                field: "ExpectedDeliveryDate",
                title: "ExpectedDeliveryDate",
                width: 150,

                },
                {
                    field: "ServiceDepartment",
                    title: "ServiceDepartment",
                    width: 150,

            },
            {
                field: "ServiceEngineer",
                title: "ServiceEngineer",
                width: 150,

                },
                {
                    field: "ServiceManager",
                    title: "ServiceManager",
                    width: 150,

            },
            {
                field: "ViewDocuments",
                title: "ViewDocuments",
                width: 150,

            }

            ]
        });

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
            query.Status = $(this).val().toLowerCase();
            // shortcode to datatable.setDataSourceParam('query', query);
            datatable.setDataSourceQuery(query);
            datatable.load();
        }).val(typeof query.Status !== 'undefined' ? query.Status : '');

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

    return {
        // public functions
        init: function () {
            demo();
        }
    };
}();

jQuery(document).ready(function () {
    DatatableRemoteAjaxDemo.init();
});