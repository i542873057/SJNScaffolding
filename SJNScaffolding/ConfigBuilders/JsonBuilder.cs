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
using System.IO;
using Newtonsoft.Json;

namespace SJNScaffolding.ConfigBuilders
{
    public class JsonBuilder : ConfigBuilder
    {
        private readonly string _configPath;

        public JsonBuilder(string configPath)
        {
            _configPath = configPath;
        }
        public override Project Build()
        {
            using (StreamReader configStream = new StreamReader(_configPath))
            {
                var jsonConfigStr = configStream.ReadToEnd();
                Project = JsonConvert.DeserializeObject<Project>(jsonConfigStr);
            }
            InitDefault();
            return Project;
        }
    }
}
