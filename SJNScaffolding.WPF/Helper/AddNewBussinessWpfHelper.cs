/*
* CLR版本:          4.0.30319.42000
* 命名空间名称/文件名:    SJNScaffolding.Helper/AddNewBussinessHelper
* 创建者：天上有木月
* 创建时间：2018/8/12 15:14:01
* 邮箱：igeekfan@foxmail.com
* 文件功能描述： 
* 
* 修改人： 
* 时间：
* 修改说明：
*/

using System.IO;
using System.Linq;
using RazorEngine;
using RazorEngine.Templating;
using SJNScaffolding.Helper;
using SJNScaffolding.Models.HelperModels;
using SJNScaffolding.Models.TemplateModels;

namespace SJNScaffolding.WPF.Helper
{
    public class AddNewBussinessWpfHelper : HelperBase<AddNewBussinessWpfHelper>
    {
        private readonly AddNewBussinessModel _parameter;
        private readonly ViewFileModel _viewFileModel;
        private readonly string _baseUrl;
        public AddNewBussinessWpfHelper(ViewFileModel viewFileModel)
        {
            this._parameter = new AddNewBussinessModel(viewFileModel.ProjectName, viewFileModel.TableName, viewFileModel.TemplateFolder);
            this._viewFileModel = viewFileModel;
            _baseUrl = viewFileModel.OutputFolder;
        }

        public void Execute()
        {
            CreateServiceInterfaceFile();
            CreateServiceFile();
            CreateDtoFiles();
            CreateViewJsFiles();

            CreateContorlFiles();

            if (_viewFileModel.WebUploadList.Any())
            {
                CreateViewModel();
            }

            CreateCoreEntity();

        }
        private void CreateServiceInterfaceFile()
        {
            var fileName = _parameter.ServiceInterfaceName + ".cs";
            var path = _parameter.TemplateBaseUrl + "\\Application\\IAppServiceTemplate.cshtml";
            var template = File.ReadAllText(path);

            string content = Engine.Razor.RunCompile(template, "CreateServiceInterfaceFile", typeof(ViewFileModel), _viewFileModel);
            CreateAndAddFile(_baseUrl + _parameter.ServiceFolder, fileName, content);
        }

        private void CreateServiceFile()
        {
            var fileName = _parameter.ServiceName + ".cs";
            var path = _parameter.TemplateBaseUrl + "\\Application\\AppServiceTemplate.cshtml";
            var template = File.ReadAllText(path);

            string content = Engine.Razor.RunCompile(template, "CreateServiceFile", typeof(ViewFileModel), _viewFileModel);
            CreateAndAddFile(_baseUrl + _parameter.ServiceFolder, fileName, content);
        }

        private void CreateViewJsFiles()
        {
            //用于标识不同的模板
            int flag = 0;

            foreach (var itemFolder in new[] { "JS", "Views" })
            {
                foreach (var templateName in new[] { "IndexTemplate", "CreateOrUpdateModalTemplate" })
                {
                    var path = _parameter.TemplateBaseUrl + "\\" + itemFolder + "\\" + templateName + ".cshtml";
                    var template = File.ReadAllText(path);
                    string content = Engine.Razor.RunCompile(template, itemFolder + templateName + "CreateViewJsFiles" + flag++, typeof(ViewFileModel), _viewFileModel);
                    string fileName = templateName.Replace("Template", ".");

                    if (itemFolder.Equals("JS"))
                    {
                        fileName += "js";
                    }
                    else
                    {
                        fileName += "cshtml";
                    }

                    CreateAndAddFile(_baseUrl + _parameter.ViewFolder, fileName, content);
                }
            }
        }

        private void CreateDtoFiles()
        {

            foreach (var templateName in new[] { "InputDtoTemplate", "ListDtoTemplate", "SearchDtoTemplate" })
            {
                var path = _parameter.TemplateBaseUrl + "\\Application\\Dto\\" + templateName + ".cshtml";
                var template = File.ReadAllText(path);
                string content = Engine.Razor.RunCompile(template, templateName + "ControllerTemplate", typeof(ViewFileModel), _viewFileModel);
                string fileName = _viewFileModel.TableName + templateName.Replace("Template", ".cs");


                CreateAndAddFile(_baseUrl + _parameter.DtoFolder, fileName, content);
            }
        }

        private void CreateContorlFiles()
        {
            var path = _parameter.TemplateBaseUrl + "\\Controllers\\ControllerTemplate.cshtml";
            var template = File.ReadAllText(path);
            string content = Engine.Razor.RunCompile(template, "CreateDtoFiles", typeof(ViewFileModel), _viewFileModel);
            string fileName = _viewFileModel.TableName + "Controller.cs";


            CreateAndAddFile(_baseUrl + _parameter.ControlFolder, fileName, content);
        }
        private void CreateViewModel()
        {
            var path = _parameter.TemplateBaseUrl + "\\ViewModel\\EntityViewModel.cshtml";
            var template = File.ReadAllText(path);
            string content = Engine.Razor.RunCompile(template, "EntityViewModel", typeof(ViewFileModel), _viewFileModel);
            string fileName = _viewFileModel.TableName + "ViewModel.cs";

            CreateAndAddFile(_baseUrl + _parameter.ViewModelFolder, fileName, content);
        }


        private void CreateCoreEntity()
        {
            var path = _parameter.TemplateBaseUrl + "\\Domain\\EntityTemplate.cshtml";
            var template = File.ReadAllText(path);
            string content = Engine.Razor.RunCompile(template, "EntityTemplate", typeof(ViewFileModel), _viewFileModel);
            string fileName = _viewFileModel.TableName + ".cs";

            CreateAndAddFile(_baseUrl + _parameter.CoreEntityFolder, fileName, content);
        }
    }
}
