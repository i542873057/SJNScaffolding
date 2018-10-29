using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RazorEngine;
using RazorEngine.Templating;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.WPF.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

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
                TypeColumnNames = TestHelper.GetColunmsList(),
                IdType = IdType.Long,
                TemplateFolder = @"..\..\..\SJNScaffolding.WPF\Templates",
                OutputFolder = @"..\..\..\SJNScaffolding.WPF\Outputs"
            };

            var bussiness = new AddNewBussinessWpfHelper(vf);

            bussiness.Execute();
        }

        [TestMethod]
        public void convertTableForm()
        {
            string a = @"Id   BigInt	唯一标识符
	                    RegNum varchar(100)	注册号
	                    UncNum	varchar(100)	社会统一信用代码
	                    EntName	varchar(100)	企业名称
	                    EntRegAddr	varchar(200)	注册地址
	                    EntActAddr	varchar(200)	实际经营地址
	                    LegalName	varchar(50)	法人姓名
	                    RegCap	decimal(10,2)	注册资本，单位万元，保留2位小数
	                    EntType	int	企业类型（单选，1-有限责任公司；2-个人独资企业；3-合伙企业；4-全民所有制企业；5-集体所有制企业；6-农民专业合作社；7-其它企业）
	                    EntScope	varchar(MAX)	经营范围
	                    TelephoneNum	varchar(50)	联系电话
	                    BankName	varchar(100)	开户银行
	                    BankNum	varchar(100)	银行账户
	                    StaffInfo	varchar(MAX)	工作人员信息
	                    WebInfo	varchar(MAX)	公司网站信息
	                    OperState varchar(MAX)	企业实际经营状况
	                    IllegalMeans	varchar(MAX)	主要违法手段
	                    InvestCount	int	投资群体人数
	                    IllegalSuckNum	decimal(18,2)	非法吸资金额，单位元，保留2位小数
	                    Remark	varchar(MAX)	备注
	                    IsDeleted	bit	是否已删除（true,false）
	                    DeleterUserId	bigint	删除操作用户Id
	                    DeletionTime	datetime	删除时间
	                    LastModificationTime	datetime	最后修改时间
	                    LastModifierUserId	bigint	最后修改操作用户Id
	                    CreationTime	datetime	创建时间
	                    CreatorUserId	bigint	创建操作用户Id
";

            List<TypeColumnName> f = TypeColumnName.String2TypeColumnNames(a);


        }
        
    }
}
