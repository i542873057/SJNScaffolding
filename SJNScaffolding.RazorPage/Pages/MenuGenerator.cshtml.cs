using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using SJNScaffolding.Models.MenuModels;
using SJNScaffolding.RazorPage.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SJNScaffolding.RazorPage.Pages
{
    public class MenuGeneratorModel : BasePageModel
    {
        private readonly MenuRoot _menuRoot;
        public string Menus { get; private set; } = "";
        public string MenusPermisson { get; private set; } = "";
        private readonly string _conn;


        public MenuGeneratorModel(IOptionsSnapshot<MenuRoot> menuRoot, IConfiguration configuration)
        {
            _menuRoot = menuRoot.Value;
            _conn = configuration.GetConnectionString("Default");
        }

        public void OnGet()
        {
            int addId = _menuRoot.BeginId;
            (StringBuilder sb, StringBuilder opSb) = GeneratorMenus(_menuRoot.MenuList, new StringBuilder(), new StringBuilder(), ref addId, _menuRoot.MenuArea, true);
            Menus = sb.ToString();
            MenusPermisson = opSb.ToString();
        }

        public (StringBuilder, StringBuilder) GeneratorMenus(List<MenuListItem> menuList, StringBuilder sb, StringBuilder opSb, ref int addId, string enCodeStr = "", bool isFirst = false)
        {
            int? parentId = null;
            if (!isFirst)
            {
                parentId = addId - 1;
            }

            if (menuList != null)
            {
                foreach (var menuListItem in menuList)
                {
                    string linkUrl = "";
                    string enCode = enCodeStr + "." + menuListItem.EnCode;
                    if (menuListItem.TypeCode == "Menu")
                    {
                        linkUrl += "/" + enCodeStr + "/" + menuListItem.TableName + "/" + menuListItem.LinkName;
                        opSb.Append($"_context.AbpMenu.Add(new SysMenu {{ ParentId = {addId}, DisplayName = \"列表权限\", EnCode = \"{enCode}.GetGrid\", TypeCode = Permission, SortCode = 1 }});").Append("\r\n");
                        opSb.Append($"_context.AbpMenu.Add(new SysMenu {{ ParentId = {addId}, DisplayName = \"新增权限\", EnCode = \"{enCode}.Add\", TypeCode = Permission, SortCode = 2 }});").Append("\r\n");
                        opSb.Append($"_context.AbpMenu.Add(new SysMenu {{ ParentId = {addId}, DisplayName = \"编辑权限\", EnCode = \"{enCode}.Edit\", TypeCode = Permission, SortCode = 3 }});").Append("\r\n");
                        opSb.Append($"_context.AbpMenu.Add(new SysMenu {{ ParentId = {addId}, DisplayName = \"删除权限\", EnCode = \"{enCode}.Delete\", TypeCode = Permission, SortCode = 4 }});").Append("\r\n");

                        enCode += "." + menuListItem.LinkName;
                    }
                    string t = $"_context.AbpMenu.Add(new SysMenu {{ ParentId = {(parentId == null ? "null" : parentId.ToString())}, DisplayName = \"{menuListItem.DisplayName}\", EnCode = \"{enCode}\", TypeCode ={menuListItem.TypeCode} , SortCode ={menuListItem.SortCode} ,LinkUrl=\"{linkUrl}\"  }});";
                    sb.Append(t).Append("\r\n");
                    ++addId;

                    GeneratorMenus(menuListItem.Children, sb, opSb, ref addId, enCodeStr);
                }
            }
            return (sb, opSb);
        }

        public IActionResult OnPostAutoComplete()
        {
            try
            {
                List<string> curEnCodeList = new List<string>();
                DataTable dt = MySqlHelper.ExecuteDataset(_conn, "select * from sysmenus").Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    curEnCodeList.Add(row["EnCode"].ToString());
                }

                AutoCompleteMenus(_menuRoot.MenuList, curEnCodeList, _menuRoot.MenuArea);
                return Content("OJBK");
            }
            catch (Exception e)
            {
                return Content("卧尼玛！报错了，信息：" + e.Message);
            }
        }

        public void AutoCompleteMenus(List<MenuListItem> menuList, List<string> curEnCodeList, string enCodeStr = "", int? parentId = null)
        {
            try
            {
                if (menuList != null)
                {
                    foreach (var menuListItem in menuList)
                    {
                        string enCode = enCodeStr + "." + menuListItem.EnCode;
                        string enCodeMenu = enCode;
                        int id = 0;
                        string linkUrl = "";
                        if (menuListItem.TypeCode == "menu")
                        {
                            linkUrl += "/" + enCodeStr + "/" + menuListItem.EnCode + "/" + menuListItem.LinkName;
                            enCodeMenu += "." + menuListItem.LinkName;
                        }

                        //如果数据库中没有这条记录，那就进行插入操作
                        if (curEnCodeList.All(u => u != enCodeMenu))
                        {
                            string sql = $"INSERT INTO `sysmenus`(`ParentId`, `DisplayName`, `EnCode`, `LinkUrl`, `Icon`, `SortCode`, `IsActive`, `TypeCode`, `Remark`, `IsDeleted`, `DeleterUserId`, `DeletionTime`, `LastModificationTime`, `LastModifierUserId`, `CreationTime`, `CreatorUserId`) VALUES ({(parentId == null ? "null" : parentId.ToString())}, '{menuListItem.DisplayName}', '{enCodeMenu}', '{linkUrl}', NULL, {menuListItem.SortCode}, 1, '{menuListItem.TypeCode}', NULL, 0, NULL, NULL, NULL, NULL, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL);select LAST_INSERT_ID();";
                            DataTable dt = MySqlHelper.ExecuteDataset(_conn, sql).Tables[0];
                            id = int.Parse(dt.Rows[0][0].ToString());

                            if (menuListItem.TypeCode == "menu")//如果是菜单的话就要添加 操作权限 
                            {
                                StringBuilder sqlBuilder = new StringBuilder();
                                sqlBuilder.Append($"INSERT INTO `sysmenus`(`ParentId`, `DisplayName`, `EnCode`, `LinkUrl`, `Icon`, `SortCode`, `IsActive`, `TypeCode`, `Remark`, `IsDeleted`, `DeleterUserId`, `DeletionTime`, `LastModificationTime`, `LastModifierUserId`, `CreationTime`, `CreatorUserId`) VALUES ({id}, '列表权限', '{enCode}.GetGrid', NULL, NULL, 1, 1, 'permission', NULL, 0, NULL, NULL, NULL, NULL, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL);");
                                sqlBuilder.Append($"INSERT INTO `sysmenus`(`ParentId`, `DisplayName`, `EnCode`, `LinkUrl`, `Icon`, `SortCode`, `IsActive`, `TypeCode`, `Remark`, `IsDeleted`, `DeleterUserId`, `DeletionTime`, `LastModificationTime`, `LastModifierUserId`, `CreationTime`, `CreatorUserId`) VALUES ({id}, '新增权限', '{enCode}.Add', NULL, NULL, 1, 1, 'permission', NULL, 0, NULL, NULL, NULL, NULL, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL);");
                                sqlBuilder.Append($"INSERT INTO `sysmenus`(`ParentId`, `DisplayName`, `EnCode`, `LinkUrl`, `Icon`, `SortCode`, `IsActive`, `TypeCode`, `Remark`, `IsDeleted`, `DeleterUserId`, `DeletionTime`, `LastModificationTime`, `LastModifierUserId`, `CreationTime`, `CreatorUserId`) VALUES ({id}, '编辑权限', '{enCode}.Edit', NULL, NULL, 1, 1, 'permission', NULL, 0, NULL, NULL, NULL, NULL, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL);");
                                sqlBuilder.Append($"INSERT INTO `sysmenus`(`ParentId`, `DisplayName`, `EnCode`, `LinkUrl`, `Icon`, `SortCode`, `IsActive`, `TypeCode`, `Remark`, `IsDeleted`, `DeleterUserId`, `DeletionTime`, `LastModificationTime`, `LastModifierUserId`, `CreationTime`, `CreatorUserId`) VALUES ({id}, '删除权限', '{enCode}.Delete', NULL, NULL, 1, 1, 'permission', NULL, 0, NULL, NULL, NULL, NULL, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL);");

                                MySqlHelper.ExecuteNonQuery(_conn, sqlBuilder.ToString());
                            }
                            StringBuilder perBuilder = new StringBuilder();
                            perBuilder.Append($"INSERT INTO `abppermissions`(`Name`, `IsGranted`, `CreationTime`, `CreatorUserId`, `UserId`, `RoleId`, `Discriminator`) VALUES ('{enCodeMenu}', 1, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL, NULL, 1, 'RolePermissionSetting');");
                            perBuilder.Append($"INSERT INTO `abppermissions`(`Name`, `IsGranted`, `CreationTime`, `CreatorUserId`, `UserId`, `RoleId`, `Discriminator`) VALUES ('{enCode}.GetGrid', 1, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL, NULL, 1, 'RolePermissionSetting');");
                            perBuilder.Append($"INSERT INTO `abppermissions`(`Name`, `IsGranted`, `CreationTime`, `CreatorUserId`, `UserId`, `RoleId`, `Discriminator`) VALUES ('{enCode}.Add', 1, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL, NULL, 1, 'RolePermissionSetting');");
                            perBuilder.Append($"INSERT INTO `abppermissions`(`Name`, `IsGranted`, `CreationTime`, `CreatorUserId`, `UserId`, `RoleId`, `Discriminator`) VALUES ('{enCode}.Edit', 1, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL, NULL, 1, 'RolePermissionSetting');");
                            perBuilder.Append($"INSERT INTO `abppermissions`(`Name`, `IsGranted`, `CreationTime`, `CreatorUserId`, `UserId`, `RoleId`, `Discriminator`) VALUES ('{enCode}.Delete', 1, '{DateTime.Now:yyyy-MM-dd hh:mm:ss}', NULL, NULL, 1, 'RolePermissionSetting');");

                            MySqlHelper.ExecuteNonQuery(_conn, perBuilder.ToString());
                        }
                        else
                        {
                            string sql = $"SELECT Id from sysmenus where encode='{enCodeMenu}';";
                            DataTable dt = MySqlHelper.ExecuteDataset(_conn, sql).Tables[0];
                            id = int.Parse(dt.Rows[0][0].ToString());
                        }

                        AutoCompleteMenus(menuListItem.Children, curEnCodeList, enCodeStr, id);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}