    $(function () {
        $("#listgrid").jqGrid({
            url: "/Admin/GetTodoLists",
            datatype: 'json',
            mtype: 'Get',
            colNames: ['CommunityID', 'CommunityName', 'CommunityDomain', 'CommunityAbout', 'CommunityLogo'],
            colModel: [
                { key: true, hidden: true, name: 'Id', index: 'Id', editable: false },
                { key: false, name: 'CommunityName', index: 'CommunityName', editable: true },
                { key: false, name: 'CommunityDomain', index: 'CommunityDomain', editable: true },
                { key: false, name: 'CommunityAbout', index: 'CommunityAbout', editable: true },
                { key: false, name: 'CommunityLogo', index: 'CommunityLogo', editable: true },
            ],
            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Todo List',
            emptyrecords: 'No records to display',
            jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            autowidth: true,
            multiselect: false
        }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
            {
                // edit options
                zIndex: 100,
                url: '/Admin/Edit',
                closeOnEscape: true,
                closeAfterEdit: true,
                recreateForm: true,
                afterComplete: function (response) {
                    if (response.responseText) {
                        alert(response.responseText);
                    }
                }
            },
            {
                // add options
                zIndex: 100,
                url: "/Admin/Create",
                closeOnEscape: true,
                closeAfterAdd: true,
                afterComplete: function (response) {
                    if (response.responseText) {
                        alert(response.responseText);
                    }
                }
            },
            {
                // delete options
                zIndex: 100,
                url: "/Admin/Delete",
                closeOnEscape: true,
                closeAfterDelete: true,
                recreateForm: true,
                msg: "Are you sure you want to delete this task?",
                afterComplete: function (response) {
                    if (response.responseText) {
                        alert(response.responseText);
                    }
                }
            });
    });
