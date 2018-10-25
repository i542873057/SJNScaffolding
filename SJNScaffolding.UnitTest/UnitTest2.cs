

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RazorEngine;
using RazorEngine.Templating;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.UnitTest.TemplateTest;

namespace SJNScaffolding.UnitTest
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
        public void testJson()
        {

            var a = new WebUploadColunm(true, UploadType.img);

            string jsonStrings = JsonConvert.SerializeObject(a);


        }


        [TestMethod]
        public void createService()
        {
            ViewFileModel vf = new ViewFileModel
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                UserName = "IGeekFan",
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                TypeColumnNames = UnitTest1.GetColunmsList(),
                IdType = IdType.LONG,
                TemplateFolder = @"..\..\..\SJNScaffolding\Templates",
                OutputFolder=@"..\..\..\SJNScaffolding\Outputs"
            };

            //var bussiness = new AddNewBussinessHelper(vf);

            //bussiness.Execute();
        }
    }
}
