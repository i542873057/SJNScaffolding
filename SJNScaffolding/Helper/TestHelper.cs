/*
  * CLR版本:          4.0.30319.42000
  * 命名空间名称/文件名:    SJNScaffolding.Core/ColunmsData
  * 创建者：天上有木月
  * 创建时间：2018/10/27 14:18:28
  * 邮箱：igeekfan@foxmail.com
  * 文件功能描述： 
  * 
  * 修改人： 
  * 时间：
  * 修改说明：
  */
using SJNScaffolding.Models.CollectiveType;
using System.Collections.Generic;
using System.Linq;

namespace SJNScaffolding.Helper
{
    /// <summary>
    /// 测试使用此类
    /// </summary>
    public class ColunmsData
    {
        public List<string> ColumnsList { get; set; }

        public List<string> ColumnsNameList { get; set; }
        public List<string> ColumnsTypeList { get; set; }


    }

    public class TestHelper
    {

        public static ColunmsData GetCommonData()
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

        public static List<TypeColumnName> GetColunmsList()
        {

            var d = GetCommonData();

            var typeNameList = new List<TypeColumnName>();
            int i = 0;
            d.ColumnsList.ForEach(r =>
            {
                WebUploadColunm webuploadColunm;
                string className = FormControl.Textbox;

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
                    className = FormControl.Combobox;
                    webuploadColunm = new WebUploadColunm();
                }
                else
                {
                    webuploadColunm = new WebUploadColunm();
                }

                if (i == 3)
                {
                    className = FormControl.Combo;
                }
                typeNameList.Add(new TypeColumnName()
                {
                    ColumnName = r,
                    AttrType = d.ColumnsTypeList[i],
                    Comments = d.ColumnsNameList[i],
                    IsRequired = i % 2 == 0 ? "1" : "0",
                    DataLength = 50 + i

                });
                i++;
            });
            return typeNameList;
        }
    }
}
