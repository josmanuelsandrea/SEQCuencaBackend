using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ScaneqCuencaBackend.Configuration.Services
{
    public static class ServiceConfiguration
    {

        public static void AddCustomServices(IServiceCollection services)
        {
            // Register BLL classes with Lazy<T> dependencies
            services.AddScoped<PDFDataBll>();
            services.AddScoped<BusOrderBll>();
            services.AddScoped<SpareOrderBll>();
            services.AddScoped<TrackingBll>();
            services.AddScoped<MaintenanceRegistryBll>();
            services.AddScoped<CustomerBll>();
            services.AddScoped<NoticeBll>();
            services.AddScoped<CooperativeBll>();
            services.AddScoped<VehicleBll>();

            // Ensure Lazy<T> dependencies are resolved
            services.AddScoped(provider => new Lazy<SpareOrderBll>(() => provider.GetRequiredService<SpareOrderBll>()));
            services.AddScoped(provider => new Lazy<BusOrderBll>(() => provider.GetRequiredService<BusOrderBll>()));
            services.AddScoped(provider => new Lazy<TrackingBll>(() => provider.GetRequiredService<TrackingBll>()));

            // Repository registrations
            services.AddScoped<BusOrdersRepository>();
            services.AddScoped<SpareOrderRepository>();
            services.AddScoped<SpareRegistryRepository>();
            services.AddScoped<SparePartRepository>();
            services.AddScoped<VehicleRepository>();
            services.AddScoped<NoticeRepository>();
            services.AddScoped<MaintenanceRegistryRepository>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<CooperativeRepository>();


            // Add other required services as needed
        }
    }
}
