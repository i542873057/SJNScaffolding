using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SJNScaffolding.ConfigBuilders;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.HelperModels;
using SJNScaffolding.Models.TemplateModels;

namespace SJNScaffolding
{
    public class ProjectBuilder : IProjectBuilder
    {
        private readonly Project _project;
        private readonly ILogger<ProjectBuilder> _logger;
        private readonly AddNewBussinessHelper _addNewBussinessHelper;
        private readonly ITemplateEngine _templateEngine;

        public ProjectBuilder(
             Project project
            , ILogger<ProjectBuilder> logger,
             AddNewBussinessHelper addNewBussinessHelper, ITemplateEngine templateEngine)
        {
            _project = project;
            _logger = logger;
            _addNewBussinessHelper = addNewBussinessHelper;
            _templateEngine = templateEngine;
        }
        public async Task Build()
        {
            foreach (var buildKv in _project.BuildTasks.Templates)
            {
                if (buildKv.IsExcute == false) continue;

                var output = buildKv.Output;

                var addViewModel = new ViewFileModel()
                {
                    TemplateFolderNames = buildKv.Key,
                    CreateTime = DateTime.Now,
                    EmailAddress = _project.EmailAddress,
                    Author = _project.Author,
                    TableName = "WebInfos",
                    ProjectName = "SJNScaffolding",
                    IdType = IdType.Long,
                    TemplateFolder = @"..\..\..\SJNScaffolding.WPF\Templates",
                    OutputFolder = _project.OutputPath
                };

                string content = await _templateEngine.Render(addViewModel);
                var fileName = Path.GetFileName(buildKv.Output.Name)??"";
                string folder = Path.GetDirectoryName(buildKv.Output.Name)??"";

                FileHelper.CreateFile(Path.Combine(_project.OutputPath, folder), fileName, content);
            }
        }

    }
}
