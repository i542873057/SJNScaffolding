/*
  * CLR版本:          4.0.30319.42000
  * 命名空间名称/文件名:    SJNScaffolding.Builders/OfficialRazorExtensions
  * 创建者：天上有木月
  * 创建时间：2018/10/28 18:35:42
  * 邮箱：igeekfan@foxmail.com
  * 文件功能描述： 
  * 
  * 修改人： 
  * 时间：
  * 修改说明：
  */
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJNScaffolding.Builders
{
    public static class OfficialRazorExtensions
    {
        public static String NewLine(this IHtmlHelper htmlHelper)
        {
            return Environment.NewLine;
        }
        public static String NewLine(this IHtmlHelper htmlHelper, string appendStr)
        {
            return Environment.NewLine + appendStr;
        }

        public static String PadLeft(this IHtmlHelper htmlHelper, int totalWidth)
        {
            return String.Empty.PadLeft(totalWidth);
        }
        public static String PadRight(this IHtmlHelper htmlHelper, int totalWidth)
        {
            return String.Empty.PadRight(totalWidth);
        }
    }
}
