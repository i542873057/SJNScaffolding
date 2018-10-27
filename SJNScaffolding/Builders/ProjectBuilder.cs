using System;
using System.Threading.Tasks;
using HandlebarsDotNet;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;

namespace SJNScaffolding.Builders
{
    public class ProjectBuilder : IProjectBuilder
    {
        private readonly Project _project;
        private readonly ITemplateEngine _templateEngine;

        public ProjectBuilder(
             IOptions<Project> p
            , ILogger<ProjectBuilder> logger,
             AddNewBussinessHelper addNewBussinessHelper, ITemplateEngine templateEngine)
        {
            _project = p.Value;
            _templateEngine = templateEngine;
        }
        public async Task Build()
        {
            try
            {
                Template[] templates = _project.BuildTasks.Templates;
                foreach (var buildKv in templates)
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
                        OutputFolder = _project.OutputPath
                    };

                    string content = await _templateEngine.Render(addViewModel);
                    var fileName = Handlebars.Compile(buildKv.Output.Name)(addViewModel) ?? "";
                    string folder = Handlebars.Compile(buildKv.Output.Folder)(addViewModel) ?? "";

                    FileHelper.CreateFile(_project.OutputPath + folder, fileName, content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
