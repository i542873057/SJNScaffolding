using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SJNScaffolding.Core;
using SJNScaffolding.Core.Helper;
using Path = System.IO.Path;

namespace SJNScaffolding
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string BasePath = @"..\..\";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TempleteProperty templeteProperty = new TempleteProperty
                {
                    ProjectName = this.ProjectName.Text,
                    TableName = this.TableName.Text
                };
                //是否有下拉，没有的话清空
                if (!this.IfCityPicker.IsChecked ?? false)
                {
                    templeteProperty.CityPickerCSSPath = "";
                    templeteProperty.CityPickerJSPath = "";
                }
                //是否有上传，没有的话清空
                if (!this.IfUpload.IsChecked ?? false)
                {
                    templeteProperty.UploadFileJSPath = "";
                }
                //模板所在文件夹
                string currentRunTimePath = Path.Combine(BasePath, "Templete");
                Dictionary<string, FileInfo> allFiles = new Dictionary<string, FileInfo>();
                FileHelper.GetFile(currentRunTimePath, allFiles);
                //获取对象所有属性
                var properties = typeof(TempleteProperty).GetProperties();
                //表中字段名
                List<string> columnsList = this.Columns.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)).Select(u => u.Trim()).ToList();
                //中文名
                List<string> columnsNameList = this.ColumnsName.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)).Select(u => u.Trim()).ToList();
                //字段填充到HTML中的内容
                List<string> columnsHtmlList = this.ColumnsHtml.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).Select(u => u.Trim()).ToList();
                //字段类型
                List<string> columnsTypeList = this.ColumnsType.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)).Select(
                    u =>
                    {
                        string conlumsType = u.Trim().ToLower();
                        //将传入的参数按程序中的类型进行转换
                        return TypeChange.typeChangeDictionary.FirstOrDefault(r => conlumsType.Contains(r.Key)).Value;
                    }).ToList();

                if (columnsList.Count != columnsTypeList.Count || columnsList.Count != columnsNameList.Count)
                {
                    throw new ArgumentException("字段之间个数不匹配！");
                }

                //为类文件填充字段
                for (var i = 0; i < columnsList.Count; i++)
                {
                    //添加注释
                    string mark = "/// <summary>" + Environment.NewLine + Environment.CommandLine+"/// " + columnsNameList[i] + Environment.NewLine + Environment.CommandLine + "/// </summary>" + Environment.NewLine;
                    templeteProperty.Content += mark + "public " + columnsTypeList[i] + " " + columnsList[i] + " { get; set; }" + Environment.NewLine;
                }
                //为CreateOrUpdateModal文件填充字段
                string baseTag = "<tr>" + Environment.NewLine + "{0}</tr>";
                string formBase = (await FileHelper.GetFileContent(Path.Combine(BasePath, "Core", "FormTemplete", "FormBase.txt"))).TrimStart().TrimEnd();//不跨行的
                string formSpecial = (await FileHelper.GetFileContent(Path.Combine(BasePath, "Core", "FormTemplete", "FormSpecial.txt"))).TrimStart().TrimEnd();//跨行的
                for (var i = 0; i < columnsList.Count; i++)
                {
                    //判断是否有跨列
                    if (columnsHtmlList[i].Contains("CrossColumn"))
                    {
                        templeteProperty.ContentHtml += string.Format(baseTag,
                            formSpecial.Replace("{{ColumnsName}}", columnsNameList[i])
                                .Replace("{{Columns}}", columnsList[i]).Replace("{{ColumnsHtml}}",
                                    columnsHtmlList[i].Replace("CrossColumn", "").Replace("Combobox", ""))) + Environment.NewLine;
                    }
                    else
                    {
                        string first = formBase.Replace("{{ColumnsName}}", columnsNameList[i])
                            .Replace("{{Columns}}", columnsList[i]).Replace("{{ColumnsHtml}}",
                                columnsHtmlList[i].Replace("CrossColumn", "")) + Environment.NewLine;
                        if (i + 1 != columnsList.Count)
                        {
                            first += formBase.Replace("{{ColumnsName}}", columnsNameList[i + 1])
                                         .Replace("{{Columns}}", columnsList[i + 1]).Replace("{{ColumnsHtml}}",
                                             columnsHtmlList[i + 1].Replace("CrossColumn", "").Replace("Combobox", "")) + Environment.NewLine;
                            i++;
                        }
                        templeteProperty.ContentHtml += string.Format(baseTag, first) + Environment.NewLine;
                    }
                }
                string comboboxPart = "";
                //生成对应的Combobox
                for (var i = 0; i < columnsHtmlList.Count; i++)
                {
                    if (columnsHtmlList[i].Contains("Combobox"))
                    {
                        comboboxPart += $"'{columnsList[i]}',";
                    }
                }
                templeteProperty.ComboboxPart = comboboxPart.TrimStart(',').TrimEnd(',');

                //按模板格式填充各字段的值
                foreach (var keyValuePair in allFiles)
                {
                    string basePathTem = "";
                    using (var read = keyValuePair.Value.OpenRead())
                    {
                        int fsLen = (int)read.Length;
                        byte[] heByte = new byte[fsLen];
                        int r = await read.ReadAsync(heByte, 0, heByte.Length);
                        string content = System.Text.Encoding.UTF8.GetString(heByte);
                        string fileName = Path.GetFileName(read.Name);
                        foreach (var propertyInfo in properties)
                        {
                            content = content.Replace("{{" + propertyInfo.Name + "}}", propertyInfo.GetValue(templeteProperty).ToString());
                            fileName = fileName?.Replace("{{" + propertyInfo.Name + "}}", propertyInfo.GetValue(templeteProperty).ToString());//生成输出的文件名
                        }
                        //根据文件夹生成对应的文件类型
                        if (read.Name.Contains("JS"))
                        {
                            basePathTem = templeteProperty.TableName;
                            fileName = fileName.Replace(".txt", ".js");
                        }
                        else if (read.Name.Contains("Views"))
                        {
                            basePathTem = templeteProperty.TableName;
                            fileName = fileName.Replace(".txt", ".cshtml");
                        }
                        else if (read.Name.Contains("Controllers"))
                        {
                            fileName = fileName.Replace(".txt", ".cs");
                        }
                        else if (read.Name.Contains("Domain"))
                        {
                            basePathTem = "Domain\\" + templeteProperty.TableName + "s";
                            fileName = fileName.Replace(".txt", ".cs");
                        }
                        else if (read.Name.Contains("Application") && !read.Name.Contains("Dto"))
                        {
                            basePathTem = "Application\\" + templeteProperty.TableName + "s";
                            fileName = fileName.Replace(".txt", ".cs");
                        }
                        else
                        {
                            basePathTem = "Application\\" + templeteProperty.TableName + "s" + "\\Dto";
                            fileName = fileName.Replace(".txt", ".cs");
                        }
                        //转换后文件输出的文件夹
                        string outputPath = Path.Combine(BasePath, "Output", basePathTem);

                        if (!Directory.Exists(outputPath))
                        {
                            Directory.CreateDirectory(outputPath);
                        }

                        // 创建文件
                        FileHelper.OutputFile(Path.Combine(outputPath, fileName), content);
                    }
                }
                MessageBox.Show("操作成功！");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
        }
    }
}
