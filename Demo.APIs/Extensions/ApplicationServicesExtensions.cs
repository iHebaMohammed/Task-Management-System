using Demo.APIs.Helper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Demo.APIs.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }
    }
}
