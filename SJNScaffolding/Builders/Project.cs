using System.Collections.Generic;

namespace SJNScaffolding.Builders
{
    public class Project
    {
        public string EmailAddress { get; set; }
        public string Author { get; set; }
        public string OutputPath { get; set; }
        public string Version { get; set; }
        public string TableName { get; set; }
        public string ProjectName { get; set; }
        public string IdType { get; set; }
        public Buildtasks BuildTasks { get; set; }

    }

    public class Buildtasks
    {
        public List<string> ProjectNames { get; set; }
        public Template[] Templates { get; set; }
    }

    public class Template
    {
        public string Key { get; set; }

        public string Remark { get; set; }

        public bool? IsExcute { get; set; }
        public Output Output { get; set; }
    }

    public class Output
    {
        /// <summary>
        /// 输出的文件夹
        /// </summary>
        public string Folder { get; set; }
        /// <summary>
        /// 输出的文件名
        /// </summary>
        public string Name { get; set; }
    }
}
