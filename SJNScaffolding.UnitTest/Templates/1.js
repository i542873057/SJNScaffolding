var gridUI = gridUI || {};
(function() {
    var _webInfoService = abp.services.app.webInfo;
    var gridUrl = '/Plat/WebInfo/GetGridByCondition';
    var editModalUrl = '/Plat/WebInfo/CreateOrUpdateModal';
    var dgGrid, dgGridId = "#dgGrid";

    $.extend(gridUI,
        {
            loadGrid: function() {
                var baseEnCode = 'Plat.WebInfo.';

                var toolbar = [
                            { text: "刷新", iconCls: "icon-reload", handler: function () { com.btnRefresh(dgGridId); } },
                            { text: "新增", EnCode: baseEnCode + 'Add', iconCls: "icon-add", handler: gridUI.btnAdd },
                            { text: "编辑", EnCode: baseEnCode + 'Edit', iconCls: "icon-edit", handler: gridUI.btnEdit },
                            { text: "删除", EnCode: baseEnCode + 'Delete', iconCls: "icon-remove", handler: gridUI.btnDelete }
                        ];
                    toolbar = com.authorizeButton(toolbar);
                   /*
_context.AbpMenu.Add(new SysMenu { ParentId = null, DisplayName = "权限",EnCode = "Plat.WebInfo", TypeCode = menu, LinkUrl = "/Plat/WebInfo/Index", SortCode = 1 });
_context.AbpMenu.Add(new SysMenu { ParentId = null, DisplayName = "列表权限",EnCode = "Plat.WebInfo.GetGrid", TypeCode = permission,  SortCode = 1 });
_context.AbpMenu.Add(new SysMenu { ParentId = null, DisplayName = "新增权限",EnCode = "Plat.WebInfo.Add", TypeCode = permission,  SortCode = 2 });
_context.AbpMenu.Add(new SysMenu { ParentId = null, DisplayName = "编辑权限",EnCode = "Plat.WebInfo.Edit", TypeCode = permission,  SortCode = 3 });
_context.AbpMenu.Add(new SysMenu { ParentId = null, DisplayName = "删除权限",EnCode = "Plat.WebInfo.Delete", TypeCode = permission, SortCode = 4});
                 */

                dgGrid = $(dgGridId).datagrid({
                    url: gridUrl,
                    toolbar: toolbar,
                    columns: [[

                         { field: 'Title', title: '标题', width: 80, formatter: function (value) {
                                if (top.clients.dataItems &&top.clients.dataItems['Title']) {
                                    return top.clients.dataItems['Title'][value];
                                } else {
                                    return '';
                                }
                            } 
                         },
                         { field: 'Content', title: '内容', width: 80},
                         { field: 'WebAddr', title: '网站地址', width: 80, formatter: function (value) {
                                if (top.clients.dataItems &&top.clients.dataItems['WebAddr']) {
                                    return top.clients.dataItems['WebAddr'][value];
                                } else {
                                    return '';
                                }
                            } 
                         },
                         { field: 'Abbreviation', title: '发布单位缩写', width: 80},
                         { field: 'Publisher', title: '发布单位', width: 80, formatter: function (value) {
                                if (top.clients.dataItems &&top.clients.dataItems['Publisher']) {
                                    return top.clients.dataItems['Publisher'][value];
                                } else {
                                    return '';
                                }
                            } 
                         },
                         { field: 'PublishTime', title: '发布时间', width: 80}
                        ]]
                });
            },
            editInfo: function(title, icon, id) {
                        var pDialog = com.dialog({
                            title: title,
                            width: 800,
                            height: 830,
                            href: editModalUrl,
                            iconCls: icon,
                            buttons: [{
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
                                        _webInfoService.createOrUpdate(formData, { showMsg: true })
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
                                    editUI.setForm(id,pDialog);
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
                        com.deleted(_webInfoService, dgGridId);
                        },
            btnAdd: function() {
                        gridUI.editInfo('新增', 'icon-add');
                        }
        });

    $(function() {
        gridUI.loadGrid();
    });

})();
