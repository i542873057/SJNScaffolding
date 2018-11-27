using System.Collections.Generic;

namespace SJNScaffolding.Models.MenuModels
{
    public class MenuListItem
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 基础资料管理
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EnCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TypeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SortCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LinkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MenuListItem> Children { get; set; }
    }
}