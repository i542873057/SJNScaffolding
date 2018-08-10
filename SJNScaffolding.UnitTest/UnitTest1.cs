using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorEngine;
using RazorEngine.Templating;
using SJNScaffolding.Core;
using SJNScaffolding.Core.Helper;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.Models.TypeCollection;

namespace SJNScaffolding.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private const string BasePath = @"..\..\..\SJNScaffolding";

        [TestMethod]
        public void TestMethod1()
        {
            TempleteProperty templeteProperty = new TempleteProperty
            {
                ProjectName = "SJNSaffolding",
                TableName = "WebInfo"
            };
            templeteProperty.CityPickerCSSPath = "";
            templeteProperty.CityPickerJSPath = "";
            templeteProperty.UploadFileJSPath = "";
            //模板所在文件夹
            string currentRunTimePath = Path.Combine(BasePath, "Templete");
            Dictionary<string, FileInfo> allFiles = new Dictionary<string, FileInfo>();
            FileHelper.GetFile(currentRunTimePath, allFiles);
            //获取对象所有属性
            var properties = typeof(TempleteProperty).GetProperties();
            var d = this.GetCommonData();


            //为类文件填充字段
            for (var i = 0; i < d.columnsList.Count; i++)
            {
                //添加注释
                string mark = "/// <summary>" + Environment.NewLine + "/// " + d.columnsNameList[i] + Environment.NewLine + "/// </summary>" + Environment.NewLine;
                templeteProperty.Content += mark + "public " + d.columnsTypeList[i] + " " + d.columnsList[i] + " { get; set; }" + Environment.NewLine;
            }

        }


        [TestMethod]
        public void testCorpyRight()
        {
            var path = @"..\..\" + "Templates\\CopyRightTemplate.cshtml";
            var template = File.ReadAllText(path);

            string content = Engine.Razor.RunCompile(template, "CopyRightTemplate", typeof(CopyRightUserInfo), new CopyRightUserInfo
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                UserName = "IGeekFan"
            });

        }

        [TestMethod]
        public void testDomainEntity()
        {
            var path = @"..\..\" + "Templates\\Domain\\EntityTemplate.cshtml";
            var template = File.ReadAllText(path);

            var d = this.GetCommonData();

            var typeNameList = new List<TypeColumnName>();
            int i = 0;
            d.columnsList.ForEach(r =>
            {
                typeNameList.Add(new TypeColumnName()
                {
                    ColumnName = r,
                    TypeName = d.columnsTypeList[i],
                    ColumnsNameRemark = d.columnsNameList[i]
                });
                i++;
            });

            string content = Engine.Razor.RunCompile(template, "EntityTemplate", typeof(ViewFileModel), new ViewFileModel
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                UserName = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType =IdType.LONG,
            });



        }


        [TestMethod]
        public void testContrrollerTemplate()
        {
            var path = @"..\..\" + "Templates\\Controllers\\ControllerTemplate.cshtml";
            var template = File.ReadAllText(path);

            var d = this.GetCommonData();

            var typeNameList = new List<TypeColumnName>();
            int i = 0;
            d.columnsList.ForEach(r =>
            {
                typeNameList.Add(new TypeColumnName()
                {
                    ColumnName = r,
                    TypeName = d.columnsTypeList[i],
                    ColumnsNameRemark = d.columnsNameList[i]
                });
                i++;
            });

            string content = Engine.Razor.RunCompile(template, "ControllerTemplate", typeof(ViewFileModel), new ViewFileModel
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                UserName = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = typeNameList,
                IdType = IdType.LONG,
            });
        }

        [TestMethod]
        public void testIndexTemplate()
        {
            var path = @"..\..\" + "Templates\\Views\\IndexTemplate.cshtml";
            var template = File.ReadAllText(path);

            var d = this.GetCommonData();

            var typeNameList = new List<TypeColumnName>();
            int i = 0;
            d.columnsList.ForEach(r =>
            {
                typeNameList.Add(new TypeColumnName()
                {
                    ColumnName = r,
                    TypeName = d.columnsTypeList[i],
                    ColumnsNameRemark = d.columnsNameList[i]
                });
                i++;
            });

            string content = Engine.Razor.RunCompile(template, "IndexTemplate", typeof(IndexViewModel), new IndexViewModel()
            {
                TableName = "WebInfos",
                IsContainUpload = true,
                WebuploadCount =4
            });
        }



        private ColunmsData GetCommonData()
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
            List<string> columnsHtmlList = new List<string>();
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
                columnsList=columnsList,
                columnsNameList =    columnsNameList,
                columnsTypeList=columnsTypeList
            };
        }

    }

    public class ColunmsData
    {
        public List<string> columnsList { get; set; }

        public List<string> columnsNameList { get; set; }
        public List<string> columnsTypeList { get; set; }
    }
}
