using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace SJNScaffolding.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// 获取路径下所有文件以及子文件夹中文件
        /// </summary>
        /// <param name="path">全路径根目录</param>
        /// <param name="fileList">存放所有文件的全路径</param>
        /// <returns></returns>
        public static Dictionary<string, FileInfo> GetFile(string path, Dictionary<string, FileInfo> fileList)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                fileList.Add(f.FullName, f);//添加文件路径到列表中
            }
            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo d in dii)
            {
                GetFile(d.FullName, fileList);
            }
            return fileList;
        }
        /// <summary>
        /// 输出文件并新建文件
        /// </summary>
        /// <param name="path">全路径根目录</param>
        /// <param name="content">输出的内容</param>
        /// <returns></returns>
        public static void OutputFile(string path, string content)
        {
            using (FileStream fs = new FileStream(path: path, mode: FileMode.OpenOrCreate, access: FileAccess.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs); //创建写入流
                sw.WriteLine(content); // 写入转换后的模板内容
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 得到文件中的内容
        /// </summary>
        /// <param name="path">全路径根目录</param>
        /// <returns></returns>
        public static async Task<string> GetFileContent(string path)
        {
            using (FileStream fs = new FileStream(path: path, mode: FileMode.Open, access: FileAccess.ReadWrite))
            {
                int fsLen = (int)fs.Length;
                byte[] heByte = new byte[fsLen];
                int r = await fs.ReadAsync(heByte, 0, heByte.Length);
                string content = System.Text.Encoding.UTF8.GetString(heByte);
                return content;
            }
        }


        public static void CreateFile(string sourePath, string fileName, string content)
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

        #region bool SaveFile(string filePath, byte[] bytes) 文件保存，
        /// <summary>
        ///  文件保存，特别是有些文件放到数据库，可以直接从数据取二进制，然后保存到指定文件夹
        /// </summary>
        /// <param name="filePath">保存文件地址</param>
        /// <param name="bytes">文件二进制</param>
        /// <returns></returns>
        public static bool SaveFile(string filePath, byte[] bytes)
        {
            bool result = true;
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    fileStream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 判断文件夹是否存在
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="path">文件夹地址</param>
        /// <returns></returns>
        public static bool DirectoryExist(string path)
        {
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 创建文件夹
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static bool DirectoryAdd(string path)
        {
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path); //新建文件夹  
                return true;
            }
            return false;
        }
        #endregion

        #region 获取压缩后的文件路径
        /// <summary>
        /// 获取压缩后的文件路径
        /// </summary>
        /// <param name="dirPath">压缩的文件路径</param>
        /// <param name="filesPath">多个文件路径</param>
        /// <returns></returns>
        public static string GetCompressPath(string dirPath, List<string> filesPath)
        {
            var zipPath = "";//返回压缩后的文件路径
            using (ZipFile zip = new ZipFile(System.Text.Encoding.Default)) //System.Text.Encoding.Default设置中文附件名称乱码，不设置会出现乱码
            {
                foreach (var file in filesPath)
                {
                    zip.AddFile(file, "");
                    //第二个参数为空，说明压缩的文件不会存在多层文件夹。比如C:\test\a\b\c.doc 压缩后解压文件会出现c.doc
                    //如果改成zip.AddFile(file);则会出现多层文件夹压缩，比如C:\test\a\b\c.doc 压缩后解压文件会出现test\a\b\c.doc
                }
                zipPath = $"{dirPath}\\{DateTime.Now:yyyyMMddHHmmss}.zip";
                zip.Save(zipPath);
            }
            return zipPath;
        }

        /// <summary>
        /// 压缩文件夹-返回压缩后的文件
        /// </summary>
        /// <param name="savepath"></param>
        /// <param name="dirPaths"></param>
        /// <returns></returns>
        public static string GetCompressDirPath(string savepath, List<string> dirPaths)
        {
            var zipPath = "";//返回压缩后的文件路径
            using (ZipFile zip = new ZipFile(System.Text.Encoding.Default)) //System.Text.Encoding.Default设置中文附件名称乱码，不设置会出现乱码
            {
                foreach (var dir in dirPaths)
                {
                    zip.AddDirectory(dir);
                }
                zipPath = $"{savepath}/{DateTime.Now:yyyyMMddHHmmss}.zip";
                zip.Save(zipPath);
            }
            return zipPath;
        }

        /// <summary>
        /// 将byte数组转换为文件并保存到指定地址
        /// </summary>
        /// <param name="buff">byte数组</param>
        /// <param name="savepath">保存地址</param>
        public static void Bytes2File(byte[] buff, string savepath)
        {
            if (File.Exists(savepath))
            {
                File.Delete(savepath);
            }

            FileStream fs = new FileStream(savepath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(buff, 0, buff.Length);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 删除文件夹下的所有文件
        /// </summary>
        /// <param name="dirRoot"></param>
        public static void DeleteFile(string dirRoot)
        {
            DirectoryInfo aDirectoryInfo = new DirectoryInfo(Path.GetDirectoryName(dirRoot));
            FileInfo[] files = aDirectoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo f in files)
            {
                File.Delete(f.FullName);
            }
        }

        public static void DeletePath(string dstPath)
        {
            try
            {
                if (!Directory.Exists(dstPath))
                    return;

                foreach (string d in Directory.GetFileSystemEntries(dstPath))
                {
                    if (File.Exists(d))
                    {
                        File.Delete(d);
                    }
                    else
                        DeletePath(d); // 递归删除子文件夹   
                }

                Directory.Delete(dstPath); 
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// 将文件转换为byte数组
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] File2Bytes(string path)
        {
            if (!File.Exists(path))
            {
                return new byte[0];
            }

            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];

            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return buff;
        }
        #endregion
    }
}
