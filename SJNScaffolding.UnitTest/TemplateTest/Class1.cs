/*
   * 创建者：天上有木月
   * 创建时间：2018/10/26 10:25:12
   * 邮箱：igeekfan@foxmail.com
   * 文件功能描述： 
   * 
   * 修改人： 
   * 时间：
   * 修改说明：
   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SJNScaffolding.WPF.UnitTest.TemplateTest
{
    [TestClass]
   public class Class1
    {
        [TestMethod]
        public void test()
        {

            var fileName = Path.GetFileName("/Application/IAppServiceTemplate.cshtml") ?? "";
            string folder = Path.GetDirectoryName("/Application/IAppServiceTemplate.cshtml") ?? "";

        }
    }
}
