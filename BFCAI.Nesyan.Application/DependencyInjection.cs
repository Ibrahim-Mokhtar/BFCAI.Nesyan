using BFCAI.Nesyan.Application.Abstraction.Services.Doctors;
using BFCAI.Nesyan.Application.Mapping;
using BFCAI.Nesyan.Application.Services.Doctors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(O =>
            {
                O.AddProfile<MappingProfile>();
            });
            services.AddScoped(typeof(IDoctorService), typeof(DoctorService));
            return services;
        }
    }
}
