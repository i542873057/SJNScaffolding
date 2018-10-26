using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorEngine;
using RazorEngine.Templating;
using SJNScaffolding.Core;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;

namespace SJNScaffolding.WPF.UnitTest.TemplateTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string BasePath = @"..\..\..\SJNScaffolding.WPF\";

        [TestMethod]
        public void TestMethod1()
        {
            TempleteProperty templeteProperty = new TempleteProperty
            {
                ProjectName = "SJNSaffolding",
                TableName = "WebInfo"
            };
            templeteProperty.CityPickerCssPath = "";
            templeteProperty.CityPickerJsPath = "";
            templeteProperty.UploadFileJsPath = "";
            //模板所在文件夹
            string currentRunTimePath = Path.Combine(BasePath, "Templates");
            Dictionary<string, FileInfo> allFiles = new Dictionary<string, FileInfo>();
            FileHelper.GetFile(currentRunTimePath, allFiles);
            //获取对象所有属性
            var properties = typeof(TempleteProperty).GetProperties();
            var d = GetCommonData();
            //为类文件填充字段
            for (var i = 0; i < d.ColumnsList.Count; i++)
            {
                //添加注释
                string mark = "/// <summary>" + Environment.NewLine + "/// " + d.ColumnsNameList[i] + Environment.NewLine + "/// </summary>" + Environment.NewLine;
                templeteProperty.Content += mark + "public " + d.ColumnsTypeList[i] + " " + d.ColumnsList[i] + " { get; set; }" + Environment.NewLine;
            }

        }


        [TestMethod]
        public void TestCorpyRight()
        {
            var path = BasePath + "Templates\\CopyRightTemplate.cshtml";
            var template = File.ReadAllText(path);

            string content = Engine.Razor.RunCompile(template, "CopyRightTemplate", typeof(CopyRightUserInfo), new CopyRightUserInfo
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan"
            });

        }

        [TestMethod]
        public void TestDomainEntity()
        {
            var path = BasePath + "Templates\\Domain\\EntityTemplate.cshtml";
            var template = File.ReadAllText(path);

            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, "EntityTemplate", typeof(ViewFileModel), new ViewFileModel
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
            });



        }


        [TestMethod]
        public void TestContrrollerTemplate()
        {
            var path = BasePath + "Templates\\Controllers\\ControllerTemplate.cshtml";
            var template = File.ReadAllText(path);


            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, "ControllerTemplate", typeof(ViewFileModel), new ViewFileModel
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
            });
        }

        [TestMethod]
        public void TestIAppservice()
        {
            //var path = BasePath + "Templates\\Application\\IAppServiceTemplate.cshtml";
            var path = BasePath + "Templates\\Application\\AppServiceTemplate.cshtml";
            var template = File.ReadAllText(path);

            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, "AppServiceTemplate", typeof(ViewFileModel), new ViewFileModel
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
                BusinessName = "信息管理"
            });
        }

        [TestMethod]
        public void TestAll()
        {
            string[] ss = new[] { "InputDtoTemplate", "ListDtoTemplate", "SearchDtoTemplate" };
            foreach (var s in ss)
            {
                TestDto(s);
            }
        }

        public void TestDto(string inputDtoTemplate = "InputDtoTemplate")
        {
            var path = BasePath + "Templates\\Application\\Dto\\" + inputDtoTemplate + ".cshtml";
            var template = File.ReadAllText(path);


            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, inputDtoTemplate, typeof(ViewFileModel), new ViewFileModel()
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
            });
        }



        [TestMethod]
        public void TestIndexTemplate()
        {
            var path = BasePath + "Templates\\Views\\IndexTemplate.cshtml";
            var template = File.ReadAllText(path);

            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, "views.IndexTemplate", typeof(ViewFileModel), new ViewFileModel()
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
            });
        }



        [TestMethod]
        public void TestIndexjsTemplate()
        {
            var path = BasePath + "Templates\\JS\\IndexTemplate.cshtml";
            var template = File.ReadAllText(path);

            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, "js.IndexTemplate", typeof(ViewFileModel), new ViewFileModel()
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
            });
        }

        [TestMethod]
        public void TestViewModelEntity()
        {
            var path = BasePath + "Templates\\ViewModel\\EntityViewModel.cshtml";
            var template = File.ReadAllText(path);

            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, "viewmodel.EntityViewModel", typeof(ViewFileModel), new ViewFileModel()
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
            });
        }

        [TestMethod]
        public void TestCreateOrUpdateModalTemplate()
        {
            var path = BasePath + "Templates\\JS\\CreateOrUpdateModalTemplate.cshtml";
            var template = File.ReadAllText(path);

            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, "CreateOrUpdateModalTemplate", typeof(ViewFileModel), new ViewFileModel()
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
            });
        }

        [TestMethod]
        public void TestViewsCreateOrUpdateModal()
        {
            var path = BasePath + "Templates\\Views\\CreateOrUpdateModalTemplate.cshtml";
            var template = File.ReadAllText(path);

            var typeNameList = GetColunmsList();

            string content = Engine.Razor.RunCompile(template, "testViewsCreateOrUpdateModal", typeof(ViewFileModel), new ViewFileModel()
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.Long,
            });
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
                    return TypeHelper.TypeChangeDictionary.FirstOrDefault(r => conlumsType.Contains(r.Key)).Value;
                }).ToList();

            return new ColunmsData
            {
                ColumnsList = columnsList,
                ColumnsNameList = columnsNameList,
                ColumnsTypeList = columnsTypeList
            };
        }

        public  static List<TypeColumnName> GetColunmsList()
        {

            var d = GetCommonData();

            var typeNameList = new List<TypeColumnName>();
            int i = 0;
            d.ColumnsList.ForEach(r =>
            {
                WebUploadColunm webuploadColunm;
                string className = EasyuiForm.Textbox;

                if (i == 0)
                {
                    webuploadColunm = new WebUploadColunm(true, r);
                }
                else if (i == 1)
                {
                    webuploadColunm = new WebUploadColunm(true, r, UploadType.Img);

                }
                else if (i == 2)
                {
                    className = EasyuiForm.Combobox;
                    webuploadColunm = new WebUploadColunm();
                }
                else
                {
                    webuploadColunm = new WebUploadColunm();
                }

                if (i == 3)
                {
                    className = EasyuiForm.Combo;
                }
                typeNameList.Add(new TypeColumnName()
                {
                    ColumnName = r,
                    TypeName = d.ColumnsTypeList[i],
                    ColumnsNameRemark = d.ColumnsNameList[i],
                    IsRequired = i % 2 == 0 ? true : false,
                    IsVarchar = true,
                    StringLength = 50 + i,
                    WebuploadColunm = webuploadColunm

                });
                i++;
            });
            return typeNameList;
        }
    }

    public class ColunmsData
    {
        public List<string> ColumnsList { get; set; }

        public List<string> ColumnsNameList { get; set; }
        public List<string> ColumnsTypeList { get; set; }
    }
}
