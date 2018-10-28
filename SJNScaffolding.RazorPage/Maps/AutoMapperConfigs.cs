using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SJNScaffolding.Builders;
using SJNScaffolding.RazorPage.Models;

namespace SJNScaffolding.RazorPage.Maps
{
    public class AutoMapperConfigs : Profile
    {
        //添加你的实体映射关系.
        public AutoMapperConfigs()
        {
            CreateMap<ProjectInputDto, Project>();
        }
    }
}
