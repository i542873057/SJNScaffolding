/*
   * 创建者：天上有木月
   * 创建时间：2018/10/26 8:57:25
   * 邮箱：igeekfan@foxmail.com
   * 文件功能描述： 
   * 
   * 修改人： 
   * 时间：
   * 修改说明：
   */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SJNScaffolding.ConfigBuilders;

namespace SJNScaffolding
{
    public class SmartCodeApp
    {
        public String AppDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public IServiceProvider ServiceProvider { get; private set; }
        public Project Project { get; private set; }
        private string ConfigPath => Project.OutputPath;
        public ILogger<SmartCodeApp> Logger { get; private set; }

        private readonly IProjectBuilder _projectBuilder;

        public SmartCodeApp(IOptions<Project> settings, 
            IServiceProvider serviceProvider,
            ILogger<SmartCodeApp> logger,
            IProjectBuilder projectBuilder
            )
        {
            Project = settings.Value;
            ServiceProvider = serviceProvider;
            Logger = logger;
            _projectBuilder = projectBuilder;
        }

        
        public async Task Run()
        {
            await _projectBuilder.Build();
        }

    }
}
