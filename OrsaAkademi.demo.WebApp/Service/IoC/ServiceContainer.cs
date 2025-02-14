using Microsoft.Extensions.DependencyInjection;
using OrsaAkademi.demo.WebApp.Models.Interface;

namespace OrsaAkademi.demo.WebApp.Service.IoC
{
    public static class ServiceContainer
    {
        public static void AddScopedService(this IServiceCollection services)
        {
            services.AddScoped<IPersonellerService,PersonellerService>();
            services.AddScoped<IGuncellemeService,GuncellemeService>();
            services.AddScoped<ITekrarliAlanService,TekrarliAlanService>();
        }


    }
}
