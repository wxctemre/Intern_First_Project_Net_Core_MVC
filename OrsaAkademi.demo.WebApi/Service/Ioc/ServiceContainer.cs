using Microsoft.Extensions.DependencyInjection;
using OrsaAkademi.demo.WebApi.model.Interface;
namespace OrsaAkademi.demo.WebApi.Service.Ioc
{
    public static class ServiceContainer
    {
        public static void AddScopedService(this IServiceCollection services)
        {

       services.AddScoped<IPersonelEklemeService, PersonelEklemeService>();
       services.AddScoped<IGuncellemeService, GuncellemeService>();
       services.AddScoped<ITekrarliAlanService, TekrarliAlandbService>();
        }

    }
}
