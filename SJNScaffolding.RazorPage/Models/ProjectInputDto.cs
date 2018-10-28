using SJNScaffolding.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJNScaffolding.RazorPage.Models
{
    public class ProjectInputDto
    {
        public string EmailAddress { get; set; }
        public string Author { get; set; }
        public string OutputPath { get; set; }
        public string Version { get; set; }
        public string TableName { get; set; }
        public string ProjectName { get; set; }
        public string IdType { get; set; }
    }
}
