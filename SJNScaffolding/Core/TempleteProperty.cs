using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJNScaffolding.Core
{
    public class TempleteProperty
    {
        #region 引用的一些文件

        /// <summary>
        /// 上传文件JS目录
        /// </summary>
        public string UploadFileJsPath { get; set; } = "<script src=\"~/bower_components/webuploader/dist/webuploader.extend.js\"></script>";

        /// <summary>
        /// 省市区下拉CSS目录
        /// </summary>
        public string CityPickerCssPath { get; set; } = "<link href=\"~/bower_components/city-picker/city-picker.css\" rel=\"stylesheet\" />";
        /// <summary>
        /// 省市区下拉JS目录
        /// </summary>
        public string CityPickerJsPath { get; set; } = "<script src=\"~/bower_components/city-picker/city-picker.data.min.js\"></script>" + Environment.NewLine + "<script src = \"~/bower_components/city-picker/city-picker.min.js\" ></ script > ";

        #endregion

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 类中的内容，一般为字段
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// HTML中的内容，一般为字段
        /// </summary>
        public string ContentHtml { get; set; }
        /// <summary>
        /// 生成对应Combobox
        /// </summary>
        public string ComboboxPart { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        public string CurrentDateTime => DateTime.Now.ToString("yyyy年MM月dd日 hh时mm分ss秒");
        /// <summary>
        /// 表名首字母小写
        /// </summary>
        public string TableNameCamel => TableName.Substring(0, 1).ToLower() + TableName.Substring(1);

        public string FileCopyRight { get; set; }
    }
}
