using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SJNScaffolding.Builders;
using System.IO;
using System.Linq;

namespace SJNScaffolding.RazorPage.Pages
{
    public class TableSettingsModel : PageModel
    {
        private readonly Project project;
        private readonly IProjectBuilder projectBuilder;
        public TableSettingsModel(IOptionsSnapshot<Project> p,IProjectBuilder projectBuilder)
        {
            project = p.Value;
            this.projectBuilder = projectBuilder;
        }

        public void OnGet()
        {

            //this.projectBuilder.AutoIncludeItems();

        }
    }
}
