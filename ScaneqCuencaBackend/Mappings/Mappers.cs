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


            CreateMap<WorkOrderRequestModel, TruckOrder>();
            CreateMap<WorkOrderEditRequestModel, TruckOrder>();
            CreateMap<TruckOrder, WorkOrderResponseModel>();

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
            // Add other mappings here
        }
    }
}
