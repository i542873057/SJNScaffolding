using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SJNScaffolding.Models.TypeCollection;

namespace SJNScaffolding.Models.TemplateModels
{
    public class CreateOrUpdateViewModel
    {
        /// <summary>
        /// 表名即类名
        /// </summary>
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
        public List<string> ComboboxPart { get; set; }
        /// <summary>
        /// 类中的属性
        /// </summary>
        public List<TypeColumnName> TypeColumnNames { get; set; }
    }
}
