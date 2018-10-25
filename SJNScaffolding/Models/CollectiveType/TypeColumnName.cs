namespace SJNScaffolding.Models.CollectiveType
{
    public class TypeColumnName
    {
        /// <summary>
        /// 属性的类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 属性名 即字段名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 属性的备注
        /// </summary>
        public string ColumnsNameRemark { get; set; }

        /// <summary>
        /// 编辑界面样式
        /// </summary>
        public string ClassName
        {
            get
            {
                if (IsCombobox)
                {
                    return EasyuiForm.Combobox;
                }
                switch (TypeName)
                {
                    case "int":
                    case "int?":
                    case "long":
                    case "long?":
                    case "decimal":
                    case "decimal?": return EasyuiForm.Numberbox;
                    case "DateTime":
                    case "DateTime?": return EasyuiForm.Datebox;
                    case "bool":
                    case "bool?": return EasyuiForm.Switchbutton;
                    case "string": return EasyuiForm.Textbox;
                    default: return "";
                }
            }
        }

        public bool IsCombobox { get; set; } = false;

        /// <summary>
        /// 是否跨三列
        /// </summary>
        private bool _isColspan3;

        public bool IsColspan3
        {
            get
            {
                if (WebuploadColunm.IsWebUpload)
                {
                    return true;
                }

                return _isColspan3;
            }
            set => _isColspan3 = value;
        }

        /// <summary>
        /// 是否是Varchar nvarchar类型
        /// </summary>
        public bool IsVarchar { get; set; } = false;
        /// <summary>
        /// varchar nvarchar 长度
        /// </summary>
        public int StringLength { get; set; } = 50;
        /// <summary>
        /// 是否是必填
        /// </summary>
        public bool IsRequired { get; set; } = false;

        public TypeColumnName()
        {
            IsColspan3 = false;
        }

        public WebUploadColunm WebuploadColunm { get; set; }

        public object DataOptions { get; set; }
        /// <summary>
        /// 属性名变成小驼峰
        /// </summary>
        public string ColumnNameCamel => this.ColumnName.Substring(0, 1).ToLower() + this.ColumnName.Substring(1, this.ColumnName.Length - 1);
    }




    public class WebUploadColunm
    {
        public string WebuploadId { get; set; }
        public WebUploadColunm()
        {
            IsWebUpload = false;
        }

        public WebUploadColunm(bool isWebUpload, string webuploadId, string uploadType = CollectiveType.UploadType.File)
        {
            WebuploadId = webuploadId;
            UploadType = uploadType;
            IsWebUpload = isWebUpload;
        }


        /// <summary>
        /// 上传类型
        /// </summary>
        public string UploadType { get; set; }
        /// <summary>
        /// 是否是上传控件
        /// </summary>
        public bool IsWebUpload { get; set; }
    }

}


