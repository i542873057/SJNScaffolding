using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RazorEngine;
using RazorEngine.Templating;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.WPF.Helper;
using SJNScaffolding.WPF.UnitTest.TemplateTest;

namespace SJNScaffolding.WPF.UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {

            var template = "";



            string content = Engine.Razor.RunCompile(template, "IndexTemplate");


        }


        [TestMethod]
        public void TestJson()
        {

            var a = new WebUploadColunm(true, UploadType.Img);

            string jsonStrings = JsonConvert.SerializeObject(a);


        }


        [TestMethod]
        public void CreateService()
        {
            ViewFileModel vf = new ViewFileModel
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                Author = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = UnitTest1.GetColunmsList(),
                IdType = IdType.Long,
                TemplateFolder = @"..\..\..\SJNScaffolding.WPF\Templates",
                OutputFolder=@"..\..\..\SJNScaffolding.WPF\Outputs"
            };

            var bussiness = new AddNewBussinessWpfHelper(vf);

            bussiness.Execute();
        }
    }
}
