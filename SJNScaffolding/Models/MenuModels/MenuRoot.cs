using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJNScaffolding.Models.MenuModels
{
    public class MenuRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string MenuArea { get; set; }
        public int BeginId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MenuListItem> MenuList { get; set; }
    }
}
