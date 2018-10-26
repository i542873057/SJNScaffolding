//*******************************
// Thx https://github.com/aspnet/Entropy/tree/master/samples/Mvc.RenderViewToString
//*******************************

using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.ObjectPool;
using SJNScaffolding.Models;
using SJNScaffolding.Models.TemplateModels;
using SJNScaffolding.Utilities;

namespace SJNScaffolding.Impl
{
    public class OfficialRazorTemplateEngine : ITemplateEngine
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OfficialRazorTemplateEngine(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task<string> Render(ViewFileModel context)
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                var helper = serviceScope.ServiceProvider.GetRequiredService<OfficialRazorViewToStringRenderer>();
                return helper.RenderViewToStringAsync(context.TemplateFolderNames, context);
            }
        }

    }
}
