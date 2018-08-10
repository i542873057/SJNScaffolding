namespace SJNScaffolding.Models.TypeCollection
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

        public bool IsColspan3 { get; set; } = false;
    }
}
