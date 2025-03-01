using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dcode.Caetering.Application.MapperProfiles
{
    public static class MapperConfiguration
    {
        public static void AddMapperConfiguration(this IServiceCollection services)
        {
            var mapper = new AutoMapper.MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new MappingProfile());
            }).CreateMapper();

            services.AddSingleton(mapper);
        }


    }
}
