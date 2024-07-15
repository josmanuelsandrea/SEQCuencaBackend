using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Work orders mapping models
            CreateMap<WorkOrderRequestModel, BusOrder>();
            CreateMap<WorkOrderEditRequestModel, BusOrder>();
            CreateMap<BusOrder, WorkOrderResponseModel>();

            // Vehicle mapping models
            CreateMap<VehicleCreateRequest, Vehicle>();
            CreateMap<VehicleEditRequest, Vehicle>();
            CreateMap<Vehicle, VehicleResponse>();

            // Customer mapping models
            CreateMap<Customer, CustomerResponse>();

            // Notice mapping models
            CreateMap<NoticeCreateRequest, Notice>();
            CreateMap<Notice, NoticeResponseModel>();
            CreateMap<NoticeUpdateRequest, Notice>();

            // MaintenanceRegistries model
            CreateMap<MaintenanceRegistryRequest, MaintenanceRegistry>();

            // Add other mappings here
        }
    }
}
