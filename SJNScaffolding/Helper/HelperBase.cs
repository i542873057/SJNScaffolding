/*
  * CLR版本:          4.0.30319.42000
  * 命名空间名称/文件名:    SJNScaffolding.Helper/HelperBase
  * 创建者：天上有木月
  * 创建时间：2018/8/12 15:15:30
  * 邮箱：igeekfan@foxmail.com
  * 文件功能描述： 
  * 
  * 修改人： 
  * 时间：
  * 修改说明：
  */
using System.IO;
using System.Text;

namespace SJNScaffolding.Helper
{
    public abstract class HelperBase<TParam>
    {

        protected void CreateAndAddFile(string sourePath, string fileName, string content)
        {
            string path = Path.GetTempPath();
            Directory.CreateDirectory(path);
            string file = Path.Combine(path, fileName);
            File.WriteAllText(file, content, Encoding.UTF8);
            try
            {
                if (string.IsNullOrEmpty(sourePath))
                {
                    sourePath = @"..\..\";
                }

                if (!File.Exists(sourePath))
                {
                    Directory.CreateDirectory(sourePath);
                }
                string soureUrl = Path.Combine(sourePath, fileName);
                if (File.Exists(soureUrl))
                {
                    File.Delete(soureUrl);
                }
                File.Copy(file, soureUrl);
            }
            finally
            {
                File.Delete(file);
            }
        }
    }
}