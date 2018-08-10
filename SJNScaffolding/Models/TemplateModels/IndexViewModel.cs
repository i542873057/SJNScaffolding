using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SJNScaffolding.Models.TypeCollection;

namespace SJNScaffolding.Models.TemplateModels
{
    public class IndexViewModel
    {
        public string WebuploadJsPath { get; set; } = "<script src=\"~/bower_components/webuploader/dist/webuploader.min.js\"></script>";

        public string WebuploadCssPath { get; set; } = "<link href=\"~/bower_components/webuploader/dist/webuploader.css\"/>";
        ///<summary>
        /// 上传文件JS目录
        /// </summary>
        public string WebUploadExtendJsPath { get; set; } = "<script src=\"~/bower_components/webuploader/dist/webuploader.extend.js\"></script>";

        /// <summary>
        /// 省市区下拉CSS目录
        /// </summary>
        public string CityPickerCSSPath { get; set; } = "<link href=\"~/bower_components/city-picker/city-picker.css\" rel=\"stylesheet\" />";
        /// <summary>
        /// 省市区下拉JS目录
        /// </summary>
        public string CityPickerJSPath { get; set; } = "<script src=\"~/bower_components/city-picker/city-picker.data.min.js\"></script>" + Environment.NewLine + "<script src = \"~/bower_components/city-picker/city-picker.min.js\" ></ script > ";

        /// <summary>
        /// 表名即类名 
        /// </summary>
        ///
        private string _tableName;


        /// <summary>
        /// 如果有s或者es则去掉
        /// </summary>
        public string TableName
        {
            get
            {
                string className = "";
                if (_tableName?.Length > 2 && (_tableName.EndsWith("es") || _tableName.EndsWith("s")))
                {

                    className = _tableName.EndsWith("es") ?
                        _tableName.Substring(0, _tableName.Length - 2) :
                        _tableName.Substring(0, _tableName.Length - 1);
                }
                return className;
            }
            set => _tableName = value;
        }

        public bool IsContainUpload { get; set; } = false;

        public bool IsCityPicker { get; set; } = false;

        public int WebuploadCount { get; set; } = 0;

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<TypeColumnName> SearchColumnNames { get; set; }
    }
}
