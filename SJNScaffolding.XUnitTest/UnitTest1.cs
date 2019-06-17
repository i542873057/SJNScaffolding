using System;
using System.Collections.Generic;
using System.IO;
using SJNScaffolding.Helper;
using Xunit;

namespace SJNScaffolding.XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test_GetCompressPathIsOk()
        {
            List<string> filesPath = new List<string>();
            filesPath.Add("E:\\svn\\T11\\TelSCode.Application\\Domain\\Plat\\MWorkInfos\\Dto\\MWorkInfoInputDto.cs");
            filesPath.Add("E:\\svn\\T11\\TelSCode.Application\\Domain\\Plat\\MWorkInfos\\Dto\\MWorkInfoListDto.cs");
            filesPath.Add("E:\\svn\\T11\\TelSCode.Application\\Domain\\Plat\\MWorkInfos\\Dto\\MWorkInfoSearchDto.cs");

            string filePath = FileHelper.GetCompressPath("E:\\svn\\T11", filesPath);
        }

        /// <summary>
        /// Ñ¹ËõÎÄ¼þ¼Ð
        /// </summary>
        [Fact]
        public void Test_GetCompressDir()
        {
            List<string> filesPath = new List<string>()
            {
                "E:\\svn\\T11"
            };

            string filePath = FileHelper.GetCompressDirPath("E:\\svn\\T11", filesPath);
        }

        [Fact]
        public void Test_Output()
        {
            string url = "E:/svn/T11/20190617092904.zip";

            string fileName = url.Substring(url.LastIndexOf("/", StringComparison.Ordinal));

        }


        [Fact]
        public void f()
        { 
            Directory.Delete("E:\\svn\\T11");

        }
    }
}
