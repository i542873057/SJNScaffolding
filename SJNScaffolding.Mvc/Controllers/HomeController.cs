using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SJNScaffolding.Core;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.Mvc.Models;

namespace SJNScaffolding.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AddNewBussinessHelper _bussinessHelper;
        public HomeController(AddNewBussinessHelper bussinessHelper)
        {
            _bussinessHelper = bussinessHelper;

        }

        public async Task<IActionResult> Index()
        {
            await _bussinessHelper.Execute(new ViewFileModel()
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                UserName = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = GetColunmsList(),
                IdType = IdType.LONG,
                TemplateFolder = @"..\..\..\SJNScaffolding\Templates",
                OutputFolder = @"D:\code\SJNScaffolding"
            });
            return View();
        }

        public static List<TypeColumnName> GetColunmsList()
        {

            var d = GetCommonData();

            var typeNameList = new List<TypeColumnName>();
            int i = 0;
            d.columnsList.ForEach(r =>
            {
                WebUploadColunm webuploadColunm;
                string className = EasyuiForm.textbox;

                if (i == 0)
                {
                    webuploadColunm = new WebUploadColunm(true, r);
                }
                else if (i == 1)
                {
                    webuploadColunm = new WebUploadColunm(true, r, UploadType.img);

                }
                else if (i == 2)
                {
                    className = EasyuiForm.combobox;
                    webuploadColunm = new WebUploadColunm();
                }
                else
                {
                    webuploadColunm = new WebUploadColunm();
                }

                if (i == 3)
                {
                    className = EasyuiForm.combo;
                }
                typeNameList.Add(new TypeColumnName()
                {
                    ColumnName = r,
                    TypeName = d.columnsTypeList[i],
                    ColumnsNameRemark = d.columnsNameList[i],
                    IsRequired = i % 2 == 0 ? true : false,
                    IsVarchar = true,
                    StringLength = 50 + i,
                    WebuploadColunm = webuploadColunm

                });
                i++;
            });
            return typeNameList;
        }


        private static ColunmsData GetCommonData()
        {
            //表中字段名
            List<string> columnsList = new List<string>()
            {
                "Title"
                ,"Content"
                ,"WebAddr"
                ,"Abbreviation"
                ,"Publisher"
                ,"PublishTime"
            };
            //中文名
            List<string> columnsNameList = new List<string>()
            {

                "标题"
                ,"内容"
                ,"网站地址"
                ,"发布单位缩写"
                ,"发布单位"
                ,"发布时间"
            };


            //字段填充到HTML中的内容
            List<string> columnsHtmlList = new List<string>()
            {
                "",
                "",
                "",
                "",
                "",
                ""
            };


            //字段类型
            List<string> columnsTypeList = new List<string>(){"nvarchar(MAX)",
                "nvarchar(MAX)",
                "nvarchar(MAX)",
                "Nvarchar(100)",
                "Nvarchar(100)",
                "Nvarchar(50)"
            }.Select(
                u =>
                {
                    string conlumsType = u.Trim().ToLower();
                    if (conlumsType.Contains("varchar"))
                    {

                    }
                    //将传入的参数按程序中的类型进行转换
                    return TypeChange.typeChangeDictionary.FirstOrDefault(r => conlumsType.Contains(r.Key)).Value;
                }).ToList();

            return new ColunmsData
            {
                columnsList = columnsList,
                columnsNameList = columnsNameList,
                columnsTypeList = columnsTypeList
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public class ColunmsData
        {
            public List<string> columnsList { get; set; }

            public List<string> columnsNameList { get; set; }
            public List<string> columnsTypeList { get; set; }
        }




    }
}
