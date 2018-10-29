using System.Collections.Generic;
using System.Threading.Tasks;
using SJNScaffolding.Models.CollectiveType;

namespace SJNScaffolding.Builders
{
    public interface IProjectBuilder
    {
        Task Build(List<TypeColumnName> typeColumnNames);
       /// <summary>
       /// 自动包含至项目
       /// </summary>
       /// <returns></returns>
        void AutoIncludeItems();
    }
}
