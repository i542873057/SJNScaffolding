using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SJNScaffolding.Models.CollectiveType
{
    public class TypeColumnName
    {
        public List<string> FieldValid { get; set; }
        //public string IsPk { get; set; }

        /// <summary>
        /// 字段类型 varchar
        /// </summary>
        public string ColumnType { get; set; }

        public int ColumnSort { get; set; }
        public string IsQuery { get; set; } = "0";
        public string IsList { get; set; } = "1";
        public string IsEdit { get; set; } = "1";

        /// <summary>
        /// 属性的类型(string,int)
        /// </summary>
        public string AttrType { get; set; }

        /// <summary>
        /// 属性名 即字段名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 属性的备注
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 编辑界面样式"easyui-numberbox"
        /// </summary>
        public string ShowType { get; set; }

        public bool IsCombobox { get; set; } = false;

        /// <summary>
        /// 是否跨三列
        /// </summary>
        private string _isColspan3;

        public string IsColspan3
        {
            get
            {
                if (WebuploadColunm != null && WebuploadColunm.IsWebUpload)
                {
                    return "1";
                }

                return _isColspan3;
            }
            set => _isColspan3 = value;
        }

        /// <summary>
        /// varchar nvarchar 长度
        /// </summary>
        public int DataLength { get; set; } = 0;

        /// <summary>
        /// 是否是必填
        /// </summary>
        public string IsRequired { get; set; } = "0";

        public TypeColumnName()
        {
            IsColspan3 = "0";
        }

        public WebUploadColunm WebuploadColunm
        {
            get
            {
                if (this.ShowType == FormControl.WebUploaderFile)
                {
                    return new WebUploadColunm(true, this.ColumnName, UploadType.File);
                }
                else if (this.ShowType == FormControl.WebUploaderImg)
                {
                    return new WebUploadColunm(true, this.ColumnName, UploadType.Img);
                }
                else
                {
                    return new WebUploadColunm();
                }
            }
        }

        public class FormOptions
        {
            public bool required { get; set; }
        }

        public string DataOptions => JsonConvert.SerializeObject(new FormOptions
        {
            required = IsRequired == "1" ? true : false
        });
        /// <summary>
        /// 属性名变成小驼峰
        /// </summary>
        public string ColumnNameCamel => this.ColumnName.Substring(0, 1).ToLower() + this.ColumnName.Substring(1, this.ColumnName.Length - 1);

        private static Dictionary<string, string> ColunmTypeAttrType()
        {
            return new Dictionary<string, string>()
            {
                {"varchar","string" },
                {"nvarchar","string" },
                {"datetime","DateTime" },
                {"datetime?","DateTime?" },
                {"int","int" },
                {"int?","int?" },
                {"long","long" },
                {"long?","long?" },
                {"decimal","decimal" },
                {"decimal?","decimal?" },
                {"bigint","long" },
                {"guid","Guid" },
                {"guid?","Guid?" },
                {"bit","bool" },
                {"bit?","bool?" },
            };
        }

        public static List<TypeColumnName> String2TypeColumnNames(String str)
        {

            string[] rows = str.Trim('\n').Split('\n');

            List<string> colunmRows = new List<string>();
            foreach (string row in rows)
            {
                string newRow = row;
                if (newRow.StartsWith("\t"))
                {
                    newRow = newRow.Substring(1, newRow.Length - 1);
                }
                newRow = newRow.Trim();
                if (newRow.EndsWith("\r"))
                {
                    newRow = newRow.Substring(0, newRow.Length - 1);
                }

                if (newRow.Length == 0) continue;
                colunmRows.Add(newRow);
            }

            List<TypeColumnName> typeColumnNames = new List<TypeColumnName>();
            Dictionary<string, string> dict = TypeColumnName.ColunmTypeAttrType();
            int i = 1;

            colunmRows?.ForEach(r =>
            {
                List<string> s = r.Replace(" ", "$").Replace("\t", "$").Split('$').Where(u => u != "").ToList();
                var column = new TypeColumnName()
                {
                    ColumnName = s.Count > 0 ? s[0] : "",
                    ColumnType = s.Count > 1 ? s[1] : "",
                    Comments = s.Count > 2 ? s[2] : "",
                    ColumnSort = i++
                };

                string coluTypelower = column.ColumnType.ToLower();
                if (column.ColumnType.IsNotNullOrEmpty() && column.ColumnType.Contains("("))
                {
                    var substring = coluTypelower.Substring(0, coluTypelower.IndexOf("(", StringComparison.Ordinal));
                    if (substring.IsNotNullOrEmpty() && dict.ContainsKey(substring))
                    {
                        column.AttrType = dict[substring];
                    }
                }
                else if (dict.ContainsKey(coluTypelower))
                {
                    column.AttrType = dict[coluTypelower];
                }

                //解析varchar(50)  得到50
                if (coluTypelower.Contains("varchar"))
                {
                    string len = coluTypelower.Replace("(", "").Replace(")", "").Replace("varchar", "").Replace("n", "");

                    int.TryParse(len, out int result);

                    if (result != 0)
                    {
                        column.DataLength = result;
                    }
                }
                //当 varchar 为max时，datalength为0， 不生成stringLength的限制
                if (coluTypelower.Contains("varchar(max)"))
                {
                    column.DataLength = 0;
                }

                switch (column.AttrType)
                {
                    case "int":
                    case "int?":
                    case "long":
                    case "long?":
                    case "decimal":
                    case "decimal?": column.ShowType = FormControl.Numberbox; break;
                    case "DateTime":
                    case "DateTime?": column.ShowType = FormControl.Datebox; break;
                    case "bool":
                    case "bool?": column.ShowType = FormControl.Switchbutton; break;
                    case "string": column.ShowType = FormControl.Textbox; break;
                    default: break;
                }

                typeColumnNames.Add(column);
            });

            return typeColumnNames;
        }
    }



    /// <summary>
    /// 上传控件属性
    /// </summary>
    public class WebUploadColunm
    {
        public string WebuploadId { get; set; }
        public WebUploadColunm()
        {
            IsWebUpload = false;
        }

        public WebUploadColunm(bool isWebUpload, string webuploadId, string uploadType = CollectiveType.UploadType.File)
        {
            WebuploadId = webuploadId;
            UploadType = uploadType;
            IsWebUpload = isWebUpload;
        }


        /// <summary>
        /// 上传类型
        /// </summary>
        public string UploadType { get; set; }
        /// <summary>
        /// 是否是上传控件
        /// </summary>
        public bool IsWebUpload { get; set; }
    }

}


