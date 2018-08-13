namespace SJNScaffolding.Models.CollectiveType
{
    public class TypeColumnName
    {
        /// <summary>
        /// 属性的类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 属性的备注
        /// </summary>
        public string ColumnsNameRemark { get; set; }

        /// <summary>
        /// 编辑界面样式
        /// </summary>
        public string ClassName { get; set; } = EasyuiForm.textbox;
        /// <summary>
        /// 是否跨三行
        /// </summary>

        public bool IsColspan3 { get; set; } = false;

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

        private bool _isWebUploadControl;
        /// <summary>
        /// 判断webuploader是否是上传控件，当样式为webupload时，一定是上传控件
        /// </summary>
        public bool IsWebUploadContorl
        {
            get
            {
                if (ClassName.Equals(EasyuiForm.WebUpload))
                {
                    return true;
                }

                return _isWebUploadControl;

            }
            set=>_isWebUploadControl=value;
        }
    }

    }


}
