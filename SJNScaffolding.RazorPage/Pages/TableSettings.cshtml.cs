using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using SJNScaffolding.Builders;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SJNScaffolding.Db;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.RazorPage.Models;
using Newtonsoft.Json;

namespace SJNScaffolding.RazorPage.Pages
{
    public class TableSettingsModel : BasePageModel
    {
        private readonly Project _project;
        private readonly IProjectBuilder _projectBuilder;
        public TableSettingsModel(IOptionsSnapshot<Project> p,IProjectBuilder projectBuilder)
        {
            _project = p.Value;
            this._projectBuilder = projectBuilder;
        }

        public void OnGet()
        {

            //this.projectBuilder.AutoIncludeItems();

        }


        public IActionResult OnPostImportTable(string formTable="")
        {
            List<TypeColumnName> columnNames = TypeColumnName.String2TypeColumnNames(formTable);

            return Json(new LayuiResultDto<TypeColumnName>(columnNames.Count,columnNames));
        }

        public IActionResult OnPostFormGenerCode(string jsonString)
        {
            List<TypeColumnName> list = JsonConvert.DeserializeObject<List<TypeColumnName>>(jsonString);

            return Json("生成代码成功!");
        }

    }
}
