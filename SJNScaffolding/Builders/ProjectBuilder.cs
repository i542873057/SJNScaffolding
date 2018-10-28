using HandlebarsDotNet;
using Microsoft.Build.Evaluation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.TemplateModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SJNScaffolding.Builders
{
    public class ProjectBuilder : IProjectBuilder
    {
        private readonly Project _project;
        private readonly ITemplateEngine _templateEngine;

        public ProjectBuilder(
             IOptionsSnapshot<Project> p
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
                        TableName = _project.TableName,
                        ProjectName = _project.ProjectName,
                        IdType = _project.IdType,
                        OutputFolder = _project.OutputPath,
                        TypeColumnNames = TestHelper.GetColunmsList()
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


        public void AutoIncludeItems()
        {

            foreach (var item in _project.BuildTasks.ProjectNames)
            {
                string projectName = Handlebars.Compile(item)(new {_project.ProjectName });

                var templates = _project.BuildTasks.Templates.ToList().Where(r => r.Output.Folder.Contains(item)).ToList();

                foreach (var buildK in templates)
                {
                    string path = Path.Combine(_project.OutputPath, projectName, projectName + ".csproj").Replace("\\","/").Replace("//","/");

                    if (!File.Exists(path))
                    {
                        continue;
                    }

                    ProjectCollection pc = new ProjectCollection();

                    var poj = pc.LoadProject(path, "15.0");

                    poj.AddItem("Compile", Path.Combine(
                        Handlebars.Compile(buildK.Output.Folder)(new { ProjectName = item }),
                        Handlebars.Compile(buildK.Output.Name)(new {_project.TableName })
                        )
                    );

                    poj.Build();
                    poj.Save();

                }

            }
        }

    }
}
