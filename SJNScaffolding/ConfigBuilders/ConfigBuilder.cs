using System;

namespace SJNScaffolding.ConfigBuilders
{
    public abstract class ConfigBuilder : IConfigBuilder
    {
        protected Project Project { get; set; }
        public abstract Project Build();

        protected void InitDefault()
        {
          
            foreach (var buildTask in Project.BuildTasks.Templates)
            {
            }
        }
    }
}
