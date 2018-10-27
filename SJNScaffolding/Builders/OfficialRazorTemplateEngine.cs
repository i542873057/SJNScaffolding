//*******************************
// Thx https://github.com/aspnet/Entropy/tree/master/samples/Mvc.RenderViewToString
//*******************************

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SJNScaffolding.Models.TemplateModels;

namespace SJNScaffolding.Builders
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
