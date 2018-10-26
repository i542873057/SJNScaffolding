using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using SJNScaffolding.Core;
using SJNScaffolding.Models.CollectiveType;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.WPF.Helper;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using RadioButton = System.Windows.Controls.RadioButton;

namespace SJNScaffolding.WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string BasePath = @"..\..\";
        private static string _idType = "long";
        public MainWindow()
        {
            InitializeComponent();
            rb_Guid.Checked += new RoutedEventHandler(radio_Checked);
            rb_int.Checked += new RoutedEventHandler(radio_Checked);
            rb_long.Checked += new RoutedEventHandler(radio_Checked);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                        return TypeHelper.TypeChangeDictionary.FirstOrDefault(r => conlumsType.Contains(r.Key)).Value;
                    }).ToList();

                if (columnsList.Count != columnsTypeList.Count || columnsList.Count != columnsNameList.Count)
                {
                    throw new ArgumentException("字段之间个数不匹配！");
                }
                //生成每个字段对应的中文名-类型-以及是否必填，是否Combobox等内容
                List<TypeColumnName> typeNameList = new List<TypeColumnName>();
                for (int i = 0; i < columnsList.Count; i++)
                {
                    if (columnsList[i].Contains("$") && columnsList[i].Contains("#"))
                    {
                        throw new ArgumentException("无法同时上传图片和文件！");
                    }
                    string columnName = columnsList[i].Replace("*", "").Replace("#", "").Replace("$", "").Replace("%", "").Replace("@", "");
                    //*是必填
                    //#是上传图片
                    //$是上传文件
                    //%是跨行
                    //@是下拉框
                    WebUploadColunm webuploadColunm;
                    if (columnsList[i].Contains("#"))
                    {
                        webuploadColunm = new WebUploadColunm(true, columnName, UploadType.Img);
                    }
                    else if (columnsList[i].Contains("$"))
                    {
                        webuploadColunm = new WebUploadColunm(true, columnName, UploadType.File);
                    }
                    else
                    {
                        webuploadColunm = new WebUploadColunm();
                    }

                    typeNameList.Add(new TypeColumnName()
                    {
                        ColumnName = columnName,
                        TypeName = columnsTypeList[i],
                        ColumnsNameRemark = columnsNameList[i],
                        IsRequired = columnsList[i].Contains("*") ? true : false,
                        IsVarchar = columnsTypeList[i] == "string",
                        StringLength = 100,
                        IsCombobox = columnsList[i].Contains("@") ? true : false,
                        IsColspan3 = columnsList[i].Contains("%") ? true : false,
                        WebuploadColunm = webuploadColunm,
                        DataOptions = columnsHtmlList[i]
                    });
                }

                ViewFileModel vf = new ViewFileModel
                {
                    CreateTime = DateTime.Now,
                    EmailAddress = this.EmailAddress.Text,
                    Author = this.UserName.Text,
                    TableName = this.TableName.Text,
                    ProjectName = this.ProjectName.Text,
                    TypeColumnNames = typeNameList,
                    IdType = _idType,
                    TemplateFolder = @"..\..\Templates",
                    OutputFolder=outputsFolder.Text??@"..\..\Outputs"
                };

                var bussiness = new AddNewBussinessWpfHelper(vf);

                bussiness.Execute();

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

        private void radio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if (btn == null)
                return;
            _idType = btn.Name.Replace("rb_", "");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string folderName = "";
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK) // Result could be true, false, or null
            {
                folderName = folderBrowser.SelectedPath;
            }
            outputsFolder.Text = folderName;
        }

    }
}
