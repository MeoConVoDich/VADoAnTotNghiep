using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Config
{
    public static class AutoMapperExtentions
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            #region Add automapper
            var assembly = Assembly.GetAssembly(typeof(AutoMapperExtentions));
            services.AddAutoMapper(assembly);
            #endregion
            services.AddAutoMapper(typeof(AppMappingProfile));
        }
    }
}
    