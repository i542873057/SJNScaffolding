namespace SJNScaffolding.ConfigBuilders
{
    public class Project
    {
        public string EmailAddress { get; set; }
        public string Author { get; set; }
        public string OutputPath { get; set; }
        public Buildtasks BuildTasks { get; set; }

    }

    public class Buildtasks
    {
        public Template[] Templates { get; set; }
    }

    public class Template
    {
        public string Key { get; set; }

        public bool? IsExcute { get; set; }
        public Output Output { get; set; }
    }

    public class Output
    {
        public string Name { get; set; }
    }
}
