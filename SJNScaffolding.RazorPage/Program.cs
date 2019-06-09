using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SJNScaffolding.RazorPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.SetBasePath(Directory.GetCurrentDirectory());
                     config.AddJsonFile("SJNScaffolding.json", optional: true, reloadOnChange: true);
                     config.AddJsonFile("MenuJson.json", optional: true, reloadOnChange: true);
                 })
                .UseUrls("http://*:5000")
                .UseStartup<Startup>();
    }
}
