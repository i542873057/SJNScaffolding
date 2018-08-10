using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine.Text;

namespace SJNScaffolding.Extend
{
    public class RazorHelper
    {
        /// <summary>
        /// 用于输出原始html
        /// </summary>
        /// <param name="value">参数</param>
        /// <returns></returns>
        public static IEncodedString RawHtml(string value)
        {
            return new RawString(value);
        }

        /// <summary>
        /// 用于输出转义后字符
        /// </summary>
        /// <param name="value">参数</param>
        /// <returns></returns>
        public static HtmlEncodedString Text(string value)
        {
            return new HtmlEncodedString(value);
        }
    }
}
