/*
  * CLR版本:          4.0.30319.42000
  * 命名空间名称/文件名:    SJNScaffolding.Models.HelperModels/AddNewBussinessModel
  * 创建者：天上有木月
  * 创建时间：2018/8/12 15:05:57
  * 邮箱：igeekfan@foxmail.com
  * 文件功能描述： 
  * 
  * 修改人： 
  * 时间：
  * 修改说明：
  */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJNScaffolding.Models.HelperModels
{
    public class AddNewBussinessModel
    {
        private readonly string _projectName;
        private readonly string _tableName;
        public string TemplateBaseUrl { get; set; }
        private string _areas = "Plat";
        /// <summary>
        /// 项目名，表名，模板根地址
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="tableName"></param>
        /// <param name="templateBaseUrl"></param>
        public AddNewBussinessModel(string projectName, string tableName, string templateBaseUrl)
        {
            _projectName = projectName;
            _tableName = tableName;
            TemplateBaseUrl = templateBaseUrl;
        }

        public string BusinessName { get; set; }


        /// <summary>
        /// 服务接口名
        /// </summary>
        public string ServiceInterfaceName => "I" + _tableName + "AppService";
        /// <summary>
        ///服务名
        /// </summary>
        public string ServiceName => _tableName + "AppService";
        /// <summary>
        /// js。页面的存放的路径 
        /// </summary>
        public string ViewFolder => this.AreasFolder+ "\\Views\\" + _tableName;

        /// <summary>
        /// 生成Service文件的路径 
        /// </summary>
        public string ServiceFolder => "\\" + this._projectName + ".Application\\Domain\\" + _areas + "\\" + _tableName + "s";
        /// <summary>
        /// 生成DTO的路径 
        /// </summary>
        public string DtoFolder => this.ServiceFolder + "\\" + "Dto";

        public string ControlFolder =>this.AreasFolder+ "\\Controllers";

        public string ViewModelFolder =>this.AreasFolder+"\\Models";


        public string AreasFolder => "\\" + this._projectName + ".Web\\Areas\\" + _areas;

        public string CoreEntityFolder => "\\" + this._projectName + ".Core\\Domain\\" + _areas + "\\" + _tableName + "s";

    }
}
