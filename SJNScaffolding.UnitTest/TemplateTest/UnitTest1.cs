using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorEngine;
using RazorEngine.Templating;
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
            var d = TestHelper.GetCommonData();
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

            var typeNameList = TestHelper.GetColunmsList();

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


            var typeNameList = TestHelper.GetColunmsList();

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

            var typeNameList = TestHelper.GetColunmsList();

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


            var typeNameList = TestHelper.GetColunmsList();

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

            var typeNameList = TestHelper.GetColunmsList();

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

            var typeNameList = TestHelper.GetColunmsList();

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

            var typeNameList = TestHelper.GetColunmsList();

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

            var typeNameList = TestHelper.GetColunmsList();

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

            var typeNameList = TestHelper.GetColunmsList();

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



    }

}
