using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorEngine;
using RazorEngine.Templating;
using SJNScaffolding.Models.TemplateModels;

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
    }
}
