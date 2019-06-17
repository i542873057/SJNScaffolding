using System.Collections.Generic;
using System.Threading.Tasks;
using SJNScaffolding.Models.CollectiveType;

namespace SJNScaffolding.Builders
{
    public interface IProjectBuilder
    {
        Task<string> Build(List<TypeColumnName> typeColumnNames,bool download=false);
        /// <summary>
        /// 自动包含至项目
        /// </summary>
        /// <returns></returns>
        void AutoIncludeItems();
    }
}
