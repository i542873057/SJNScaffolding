using System.Threading.Tasks;

namespace SJNScaffolding.Builders
{
    public interface IProjectBuilder
    {
        Task Build();
       /// <summary>
       /// 自动包含至项目
       /// </summary>
       /// <returns></returns>
        void AutoIncludeItems();
    }
}
