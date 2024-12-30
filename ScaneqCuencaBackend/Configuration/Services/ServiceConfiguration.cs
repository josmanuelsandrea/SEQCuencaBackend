using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ScaneqCuencaBackend.Configuration.Services
{
    public static class ServiceConfiguration
    {

        public static void AddCustomServices(IServiceCollection services)
        {
            // PDF Services
            services.AddScoped<PDFDataBll>();

            // BusOrders Services
            services.AddScoped<BusOrdersRepository>();

            services.AddScoped<BusOrderBll>();

            // Cooperative services
            services.AddScoped<CooperativeBll>();
            services.AddScoped<CooperativeRepository>();

            // Customer services
            services.AddScoped<CustomerBll>();
            services.AddScoped<CustomerRepository>();

            // MaintenanceRegistry services
            services.AddScoped<MaintenanceRegistryBll>();
            services.AddScoped<MaintenanceRegistryRepository>();

            // Notice services
            services.AddScoped<NoticeBll>();
            services.AddScoped<NoticeRepository>();

            // SpareOrder services
            services.AddScoped<SpareOrderBll>();

            services.AddScoped<SpareOrderRepository>();
            services.AddScoped<SpareRegistryRepository>();
            services.AddScoped<SparePartRepository>();

            // Tracking services
            services.AddScoped<TrackingBll>();

            // Vehicle services
            services.AddScoped<VehicleBll>();
            services.AddScoped<VehicleRepository>();
        }
    }
}
