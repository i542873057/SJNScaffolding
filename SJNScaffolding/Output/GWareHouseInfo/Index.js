
var gridUI = gridUI || {};
(function() {
    var processService = abp.services.app.gWareHouseInfo;
    var gridUrl = '/Plat/GWareHouseInfo/GetGridByCondition';
    var editModalUrl = '/Plat/GWareHouseInfo/CreateOrUpdateModal';
    var dgGrid, dgGridId = "#dgGrid";

    $.extend(gridUI,
        {
            loadGrid: function() {
                dgGrid = $(dgGridId).datagrid({
                    url: gridUrl,
                    toolbar: [
                        { text: "刷新", iconCls: "icon-reload", handler: function() { com.btnRefresh(dgGridId); } },
                        { text: "新增", iconCls: "icon-add", handler: gridUI.btnAdd },
                        { text: "编辑", iconCls: "icon-edit", handler: gridUI.btnEdit },
                        { text: "删除 ", iconCls: "icon-remove", handler: gridUI.btnDelete }
                    ],
                    columns: [
                        [
                            { field: 'StoreName', title: '仓库名称', width: 120 },
                            {field: 'LinkMan',title: '联系人',width: 120},
                            {field: 'ContactNum',title: '联系方式',width: 80},
                            { field: 'CreationTime', title: '增加时间', width: 80 }
                        ]
                    ]
                });
            },
            editInfo: function(title, icon, id) {
                var editModalUrlTemp = editModalUrl;
                if (id) {
                    editModalUrlTemp += "/" + id;
                }
                var pDialog = com.dialog({
                    title: title,
                    width: 800,
                    height: 830,
                    href: editModalUrlTemp,
                    iconCls: icon,
                    buttons: [
                        {
                            text: '确认',
                            iconCls: icon,
                            handler: function() {
                                var f = pDialog.find('#editForm');
                                var isValid = f.form('validate');
                                if (!isValid) {
                                    return;
                                }
                                var formData = f.formSerialize();
                                com.setBusy(pDialog, true);
                                processService.createOrUpdate(formData, { showMsg: true })
                                    .done(function() {
                                        com.btnRefresh();
                                        pDialog.dialog('close');
                                    })
                                    .always(function() {
                                        com.setBusy(pDialog, false);
                                    });
                            }
                        }
                    ],
                    onLoad: function() {
                        editUI.setForm(id);
                    }
                });
            },
            btnEdit: function() {
                com.edit(dgGridId,
                    function(id) {
                        gridUI.editInfo("编辑", 'icon-edit', id);
                    });
            },
            btnDelete: function() {
                com.deleted(processService, dgGridId);
            },
            btnAdd: function() {
                gridUI.editInfo('新增', 'icon-add');
            }
        });

    $(function() {
        gridUI.loadGrid();
    });

})();

