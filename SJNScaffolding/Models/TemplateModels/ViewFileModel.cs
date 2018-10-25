using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SJNScaffolding.Models.CollectiveType;

namespace SJNScaffolding.Models.TemplateModels
{
    public class ViewFileModel : CopyRightUserInfo
    {
        public string TemplateNames { get; set; }
        /// <summary>
        /// 业务名-如用户管理
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 模板路径
        /// </summary>

        public string TemplateFolder { get; set; }
        /// <summary>
        /// 生成代码路径 
        /// </summary>
        public string OutputFolder { get; set; }

        /// <summary>
        /// 项目名称即数据库名
        /// </summary>
        public string ProjectName { get; set; }

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
                if (_tableName?.Length > 2 && (_tableName.EndsWith("es") || _tableName.EndsWith("s")))
                {
                    var className = _tableName.EndsWith("es") ?
                        _tableName.Substring(0, _tableName.Length - 2) :
                        _tableName.Substring(0, _tableName.Length - 1);
                    return className;
                }
                return _tableName;
            }
            set => _tableName = value;
        }
        /// <summary>
        /// id的类型
        /// </summary>
        public string IdType { get; set; } = CollectiveType.IdType.Int;
        /// <summary>
        /// 首字母小写
        /// </summary>
        public string TableNameCamel => this.TableName.Substring(0, 1).ToLower() + this.TableName.Substring(1, this.TableName.Length - 1);
        /// <summary>
        /// 类中的属性
        /// </summary>
        public List<TypeColumnName> TypeColumnNames { get; set; }

        /// <summary>
        /// 是否需要配置权限
        /// </summary>
        public bool HasPermission { get; } = true;

        public string ComboboxPart
        {
            get
            {
                List<dynamic> comboboxPart = new List<dynamic>();
                TypeColumnNames?.ForEach(r =>
                {
                    if (r.ClassName == EasyuiForm.Combo || r.ClassName == EasyuiForm.Combobox)
                    {
                        comboboxPart.Add(new
                        {
                            ClassName = r.ClassName.Replace("easyui-",""),
                            r.ColumnName
                        });
                    }
                });
                var comboxControl = JsonConvert.SerializeObject(comboboxPart);
                return comboxControl;
            }
        }
        public string WebUploadPart
        {
            get
            {
                var webuploadControl = JsonConvert.SerializeObject(this.WebUploadList);
                return webuploadControl;
            }
        }

        public List<WebUploadColunm> WebUploadList
        {
            get
            {
                List<WebUploadColunm> webuploadPart = new List<WebUploadColunm>();
                TypeColumnNames?.ForEach(r =>
                {
                    if (r.WebuploadColunm.IsWebUpload)
                    {
                        webuploadPart.Add(r.WebuploadColunm);
                    }
                });
                return webuploadPart;
            }
        }

        
        /// <summary>
        /// 查询条件
        /// </summary>
        public List<TypeColumnName> SearchColumnNames { get; set; }
    }
}