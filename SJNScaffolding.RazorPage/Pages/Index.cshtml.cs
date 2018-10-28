using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SJNScaffolding.Builders;
using SJNScaffolding.Db;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.RazorPage.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SJNScaffolding.RazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProjectBuilder _projectBuilder;
        private readonly AddNewBussinessHelper _addNewBussinessHelper;
        private readonly ITemplateEngine _templateEngine;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public Project Project { get; private set; }

        public IndexModel(IOptionsSnapshot<Project> p, IProjectBuilder projectBuilder, ITemplateEngine templateEngine, AddNewBussinessHelper addNewBussinessHelper, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _projectBuilder = projectBuilder;
            _templateEngine = templateEngine;
            _addNewBussinessHelper = addNewBussinessHelper;
            Project = p.Value;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostSaveSettings(ProjectInputDto projectInputDto)
        {
            var project = _mapper.Map<Project>(projectInputDto);
            project.BuildTasks = Project.BuildTasks;

            System.IO.File.WriteAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "SJNScaffolding.json"), JObject.FromObject(new { Project = project }).ToString());

            return Json(new LayuiResultDto("保存配置成功!"));
        }


        public IActionResult OnPost()
        {

            var templates = Project.BuildTasks?.Templates.ToList();

            return Json(new LayuiResultDto<Template>(templates.Count, templates));

        }

        public async Task<IActionResult> OnPostCodeGeneratorAsync()
        {
            await _projectBuilder.Build();
            return Json(new LayuiResultDto("生成成功！"));
        }


        private IActionResult Json(object o)
        {
            return Content(JsonConvert.SerializeObject(o));
        }
        public async Task<ActionResult> Get()
        {
            ViewFileModel _viewFileModel = new ViewFileModel()
            {
                TemplateFolderNames = "/Application/Dto/ListDtoTemplate.cshtml",
                CreateTime = DateTime.Now,
                EmailAddress = Project.EmailAddress,
                Author = Project.Author,
                TableName = "WebInfos",
                ProjectName = "SJNScaffolding",
                IdType = IdType.Long,
                TemplateFolder = @"..\..\..\SJNScaffolding.WPF\Templates",
                OutputFolder = Project.OutputPath,
                TypeColumnNames = TestHelper.GetColunmsList()
            };

            foreach (var itemFolder in new[] { "Views", "JS" })
            {
                foreach (var templateName in new[] { "IndexTemplate", "CreateOrUpdateModalTemplate" })
                {
                    _viewFileModel.TemplateFolderNames = "/Views/" + itemFolder + "-" + templateName + ".cshtml";
                    string content = await _templateEngine.Render(_viewFileModel);

                    string fileName = templateName.Replace("JS", "").Replace("Template", ".").Replace("Views", ".");

                    if (itemFolder.Equals("JS"))
                    {
                        fileName += "js";
                    }
                    else
                    {
                        fileName += "cshtml";
                    }

                    //  FileHelper.CreateFile(_baseUrl + _parameter.ViewFolder, fileName, content);
                }
            }
            //await _addNewBussinessHelper.Execute(new ViewFileModel()
            //{
            //    TemplateFolderNames = "/Application/Dto/ListDtoTemplate.cshtml",
            //    CreateTime = DateTime.Now,
            //    EmailAddress = _project.EmailAddress,
            //    Author = _project.Author,
            //    TableName = "WebInfos",
            //    ProjectName = "SJNScaffolding",
            //    IdType = IdType.Long,
            //    TemplateFolder = @"..\..\..\SJNScaffolding.WPF\Templates",
            //    OutputFolder = _project.OutputPath,
            //    TypeColumnNames = TestHelper.GetColunmsList()
            //});
            return Content("OJBK");
        }

    }
}

