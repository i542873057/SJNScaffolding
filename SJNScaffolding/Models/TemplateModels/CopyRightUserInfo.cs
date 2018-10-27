using System;

namespace SJNScaffolding.Models.TemplateModels
{
    /// <summary>
    /// 版权所有
    /// </summary>
   public class CopyRightUserInfo
    {
        /// <summary>
        /// 开发者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 文件备注
        /// </summary>
        public string FileRemark { get; set; }
    }
}
