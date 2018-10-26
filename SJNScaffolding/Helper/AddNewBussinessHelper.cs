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
using SJNScaffolding.Models.HelperModels;
using SJNScaffolding.Models.TemplateModels;
using System.Linq;
using System.Threading.Tasks;

namespace SJNScaffolding.Helper
{
    public class AddNewBussinessHelper
    {
        private AddNewBussinessModel _parameter;
        private ViewFileModel _viewFileModel;
        private string _baseUrl;
        private readonly ITemplateEngine _templateEngine;
        public AddNewBussinessHelper(ITemplateEngine templateEngine)
        {
            _templateEngine = templateEngine;
        }

        public async Task Execute(ViewFileModel viewFileModel)
        {
            this._parameter = new AddNewBussinessModel(viewFileModel.ProjectName, viewFileModel.TableName, viewFileModel.TemplateFolder);
            this._viewFileModel = viewFileModel;
            this._baseUrl = viewFileModel.OutputFolder;

            await CreateServiceInterfaceFile();
            await CreateServiceFile();
            await CreateDtoFiles();
            await CreateViewJsFiles();
            await CreateContorlFiles();

            if (_viewFileModel.WebUploadList.Any())
            {
                await CreateViewModel();
            }

            await CreateCoreEntity();

        }
        private async Task CreateServiceInterfaceFile()
        {
            _viewFileModel.TemplateFolderNames = "/Application/IAppServiceTemplate.cshtml";
            string content = await _templateEngine.Render(_viewFileModel);
            var fileName = _parameter.ServiceInterfaceName + ".cs";
            FileHelper.CreateFile(_baseUrl + _parameter.ServiceFolder, fileName, content);
        }

        private async Task CreateServiceFile()
        {
            _viewFileModel.TemplateFolderNames = "/Application/AppServiceTemplate.cshtml";

            var fileName = _parameter.ServiceName + ".cs";

            string content = await _templateEngine.Render(_viewFileModel);

            FileHelper.CreateFile(_baseUrl + _parameter.ServiceFolder, fileName, content);
        }

        private async Task CreateViewJsFiles()
        {
            foreach (var itemFolder in new[] { "JS", "Views" })
            {
                foreach (var templateName in new[] { "IndexTemplate", "CreateOrUpdateModalTemplate" })
                {
                    _viewFileModel.TemplateFolderNames = "/" + itemFolder + "/" + templateName + ".cshtml";
                    string content = await _templateEngine.Render(_viewFileModel);

                    string fileName = templateName.Replace("Template", ".");

                    if (itemFolder.Equals("JS"))
                    {
                        fileName += "js";
                    }
                    else
                    {
                        fileName += "cshtml";
                    }

                    FileHelper.CreateFile(_baseUrl + _parameter.ViewFolder, fileName, content);
                }
            }
        }

        private async Task CreateDtoFiles()
        {
            foreach (var templateName in new[] { "InputDtoTemplate", "ListDtoTemplate", "SearchDtoTemplate" })
            {
                _viewFileModel.TemplateFolderNames = "/Application/Dto/" + templateName + ".cshtml";
                string content = await _templateEngine.Render(_viewFileModel);
                string fileName = _viewFileModel.TableName + templateName.Replace("Template", ".cs");
                FileHelper.CreateFile(_baseUrl + _parameter.DtoFolder, fileName, content);
            }
        }

        private async Task CreateContorlFiles()
        {
            _viewFileModel.TemplateFolderNames = "/Controllers/ControllerTemplate.cshtml";
            string content = await _templateEngine.Render(_viewFileModel);
            string fileName = _viewFileModel.TableName + "Controller.cs";
            FileHelper.CreateFile(_baseUrl + _parameter.ControlFolder, fileName, content);
        }
        private async Task CreateViewModel()
        {
            _viewFileModel.TemplateFolderNames = "/ViewModel/EntityViewModel.cshtml";
            string content = await _templateEngine.Render(_viewFileModel);
            string fileName = _viewFileModel.TableName + "ViewModel.cs";

            FileHelper.CreateFile(_baseUrl + _parameter.ViewModelFolder, fileName, content);
        }


        private async Task CreateCoreEntity()
        {
            _viewFileModel.TemplateFolderNames = "/Domain/EntityTemplate.cshtml";
            string content = await _templateEngine.Render(_viewFileModel);
            string fileName = _viewFileModel.TableName + ".cs";

            FileHelper.CreateFile(_baseUrl + _parameter.CoreEntityFolder, fileName, content);
        }
    }
}
