using System;
using System.Threading.Tasks;
using SJNScaffolding.Models;
using SJNScaffolding.Models.TemplateModels;

namespace SJNScaffolding
{
    public interface ITemplateEngine 
    {
        Task<string> Render(ViewFileModel context);
    }
}
