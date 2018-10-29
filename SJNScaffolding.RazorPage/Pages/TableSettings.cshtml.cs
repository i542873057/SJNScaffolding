using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SJNScaffolding.Builders;
using SJNScaffolding.Db;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.RazorPage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SJNScaffolding.RazorPage.Pages
{
    public class TableSettingsModel : BasePageModel
    {
        private readonly Project _project;
        private readonly IProjectBuilder _projectBuilder;
        public TableSettingsModel(IOptionsSnapshot<Project> p, IProjectBuilder projectBuilder)
        {
            _project = p.Value;
            this._projectBuilder = projectBuilder;
        }

        public void OnGet()
        {
            //this.projectBuilder.AutoIncludeItems();

        }


        public IActionResult OnPostImportTable(string formTable = "")
        {
            List<TypeColumnName> columnNames = TypeColumnName.String2TypeColumnNames(formTable);

            return Json(new LayuiResultDto<TypeColumnName>(columnNames.Count, columnNames));
        }

        public async Task<IActionResult> OnPostFormGenerCodeAsync(string jsonString)
        {
            List<TypeColumnName> list = JsonConvert.DeserializeObject<List<TypeColumnName>>(jsonString);

            await _projectBuilder.Build(list);

            return Success("生成代码成功!");
        }

    }
}
